using System;
using System.Collections;
using Microsoft.SPOT.Hardware;

/*
 * some general thoughts on types of button events to catch:
 * 1) single press of single button (handled by AgentButtonListener)
 * 2) double press of single button 
 * 3) triple press of single button 
 * 4) long single press of single button
 * 5) single press of a combination of buttons (handled by CombinationAgentButtonListener)
 */

namespace AGENT.Contrib.Hardware
{
	public delegate void CombinationAgentButtonStateChangeEventHandler (Hashtable previousButtons, Hashtable currentButtons, AgentButton changingButton, AgentButtonState changingButtonState, DateTime time);
	
	/// <summary>
	/// Handles detection of simultaneous button presses.<br/>
	/// NOTE: Consuming classes will have issues if they are detecting both double and triple presses (IE, (A+B | A+C | B+C) & A+B+C), because the double button event will occur first, followed by the triple button event.
	/// </summary>
    public class CombinationAgentButtonListener
    {
	    public event CombinationAgentButtonStateChangeEventHandler OnButtonStateChange;

	    private AgentButtonListener m_Listener;

		/// <summary>
		/// Used so that we have a single reference for listener event handler
		/// </summary>
		private readonly AgentButtonStateChangeEventHandler m_AgentButtonListener;

		/// <summary>
		/// Map of AgentButton to AgentButtonState
		/// </summary>
	    private readonly Hashtable m_ButtonToStateMap;

		public CombinationAgentButtonListener () : this (AgentButtonListener.Current, AgentButton.Empty.GetAllRight ())
		{
		}

		public CombinationAgentButtonListener (AgentButtonListener listener, params AgentButton[] buttons)
		{
			if (listener == null)
				throw new ArgumentNullException ("listener");

			m_Listener = listener;

			m_ButtonToStateMap = new Hashtable();
			foreach (var b in AgentButton.Empty.GetAll ())
				m_ButtonToStateMap[b] = AgentButtonState.Up;

			listener.StartListeningTo (buttons);
			m_AgentButtonListener = OnAgentButtonListenerPressed;
			listener.OnButtonStateChange += m_AgentButtonListener;
        }

		~CombinationAgentButtonListener ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
		}

		private void Dispose (bool disposing)
		{
			// do this first so that no more events will fire
			OnButtonStateChange = null;

			if (disposing)
			{
				// manually dispose of any directly referenced resources
				m_Listener.OnButtonStateChange -= m_AgentButtonListener;
			}

			m_Listener = null;
		}

		private void OnAgentButtonListenerPressed (AgentButton button, InterruptPort port, AgentButtonState state, DateTime time)
		{
			var previousPressedButtons = GetPressedButtons ();
			m_ButtonToStateMap[button] = state;
			var currentPressedButtons = GetPressedButtons ();

			if (OnButtonStateChange == null) return; // no listeners

			OnButtonStateChange (previousPressedButtons, currentPressedButtons, button, state, time);
		}

		private Hashtable GetPressedButtons ()
	    {
			var pressedButtons = new Hashtable ();
			foreach (var b in AgentButton.Empty.GetAll ())
				if ((AgentButtonState) m_ButtonToStateMap[b] == AgentButtonState.Down)
					pressedButtons[b] = b;
		    return pressedButtons;
	    }
    }
}
