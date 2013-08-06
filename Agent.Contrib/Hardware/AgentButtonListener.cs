using System;
using System.Collections;
using Microsoft.SPOT.Hardware;

namespace AGENT.Contrib.Hardware
{
	public delegate void AgentButtonStateChangeEventHandler (AgentButton button, InterruptPort port, AgentButtonState state, DateTime time);

	/// <summary>
	/// A simple class to abstract away the use of InterruptPorts (aka Buttons)
	/// </summary>
	public class AgentButtonListener : IDisposable
	{
		private static readonly object s_Lock = new object ();
		private static AgentButtonListener s_Current;

		public static AgentButtonListener Current
		{
			get
			{
				lock (s_Lock)
				{
					if (s_Current == null)
					{
						s_Current = new AgentButtonListener ();
					}
				}
				return s_Current;
			}
		}

		public event AgentButtonStateChangeEventHandler OnButtonStateChange;

		private readonly Object m_Lock = new object ();

		/// <summary>
		/// Used so that we have a single reference for all interrupt event listeners
		/// </summary>
		private NativeEventHandler m_InterruptHandler;

		/// <summary>
		/// Map of AgentButton to AgentButton (used since HashSet isn't available)
		/// </summary>
		private Hashtable m_WatchedButtons;

		/// <summary>
		/// Map of Cpu.Pin Interrupt Port ID to InterruptPort
		/// </summary>
		private Hashtable m_InterruptPortIdToInterruptPortMap;

		public AgentButtonListener ()
		{
			m_InterruptHandler = OnInterrupt;
			m_InterruptPortIdToInterruptPortMap = new Hashtable ();
			m_WatchedButtons = new Hashtable ();
		}

		public AgentButtonListener (params AgentButton[] buttons)
			: this ()
		{
			if (buttons == null) return;

			StartListeningTo (buttons);
		}

		~AgentButtonListener ()
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
			}

			foreach (DictionaryEntry b in m_WatchedButtons)
				StopListeningTo ((AgentButton) b.Key);

			m_WatchedButtons.Clear ();
			m_WatchedButtons = null;
			m_InterruptPortIdToInterruptPortMap.Clear ();
			m_InterruptPortIdToInterruptPortMap = null;
			m_InterruptHandler = null;
		}

		public void StartListeningTo (params AgentButton[] buttons)
		{
			foreach (var b in buttons)
			{
				StartListeningTo (b);
			}
		}

		public void StartListeningTo (AgentButton b)
		{
			if (m_WatchedButtons.Contains (b)) return; // already being watched

			lock (m_Lock)
			{
				if (m_WatchedButtons.Contains (b)) return; // already being watched

				m_WatchedButtons[b] = b;

				var ip = new InterruptPort ((Cpu.Pin) b, false, Port.ResistorMode.PullUp, Port.InterruptMode.InterruptEdgeBoth);
				m_InterruptPortIdToInterruptPortMap[ip.Id] = ip;
				ip.OnInterrupt += m_InterruptHandler;
			}
		}

		private void OnInterrupt (uint data1, uint data2, DateTime time)
		{
			if (OnButtonStateChange == null // no listeners
			    || !m_InterruptPortIdToInterruptPortMap.Contains ((Cpu.Pin) data1)) // not listening to the given port (shouldn't have fired anyway)
				return;

			var ip = (InterruptPort) m_InterruptPortIdToInterruptPortMap[(Cpu.Pin) data1];
			//data1 is the is the number of the pin of the switch
			//data2 is the value if the button is pushed or released; 0 = down, 1 = up
			OnButtonStateChange ((AgentButton) data1, ip, (AgentButtonState) data2, time);
		}

		public bool IsListeningTo (AgentButton button)
		{
			return m_WatchedButtons.Contains (button);
		}

		public void StopListeningTo (params AgentButton[] buttons)
		{
			foreach (var b in buttons)
			{
				StopListeningTo (b);
			}
		}

		public void StopListeningTo (AgentButton b)
		{
			if (!m_WatchedButtons.Contains (b)) return; // already not watching

			lock (m_Lock)
			{
				if (!m_WatchedButtons.Contains (b)) return; // already not watching

				m_WatchedButtons.Remove (b);

				var ip = (InterruptPort) m_InterruptPortIdToInterruptPortMap[(Cpu.Pin) b];
				if (ip == null) return;

				m_InterruptPortIdToInterruptPortMap.Remove ((Cpu.Pin) b);
				ip.OnInterrupt -= m_InterruptHandler;
				ip.Dispose ();
			}
		}
	}
}