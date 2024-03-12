        internal virtual void CheckACservices()
        {
            ret:;

            foreach (var proc in System.Diagnostics.Process.GetProcesses())
            {
                var processName = proc.ProcessName.Trim();
                if (processName.Contains("faceit"))
                {
                    Console.WriteLine("PROCESS_BLOCK_EXCEPTION -> " + PROCESS_BLOCK_EXCEPTION.FACEIT);
                    MessageBox.Show("Faceit açıkken exelent çalışmaya devam etmez.\nHataya neden olan anti hile programını devre dışı bırakıp tamama basın.", "exelent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    goto ret;
                }
                if (processName.Contains("vanguard"))
                {
                    Console.WriteLine("PROCESS_BLOCK_EXCEPTION -> " + PROCESS_BLOCK_EXCEPTION.VANGUARD);
                    MessageBox.Show("Vanguard açıkken exelent çalışmaya devam etmez.\nHataya neden olan anti hile programını devre dışı bırakıp tamama basın.", "exelent", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    goto ret;
                }
            }
        }

        enum PROCESS_BLOCK_EXCEPTION
        {
            FACEIT = 1,
            VANGUARD = 2,
        }
