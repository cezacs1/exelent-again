        C_ConsoleCommand g_Console = memory.Read<C_ConsoleCommand>(memory.tier0 + 0x370100);
        Console.WriteLine("g_Console: " + g_Console);
        Console.WriteLine("g_Console.cmd_list_count: " + g_Console.cmd_list_count);
        Console.WriteLine("g_Console.cmd_lists: " + g_Console.cmd_lists);
        Console.WriteLine("g_Console.commands: " + g_Console.commands);
        g_Console.Dump();

////////////////////////////////////////////////////////////////////////////////////////////////

        public struct C_ConsoleCommand
        {
            public IntPtr vtable;
            public ulong cmd_list_count;
            public IntPtr cmd_lists;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0xC0)]
            public byte[] pad0;
            public IntPtr uk1;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x38)]
            public byte[] pad1;
            public IntPtr commands;

            public void Dump()
            {
                for (int i = 0; i < (int)cmd_list_count; i++)
                {
                    char previus = '\0';
                    for (int x = 0; x < 0x7F0/*9999*/; x++)
                    {
                        if (memory.Read<int>((ulong)(cmd_lists + i * 0x7f0 + x)) != 0 && previus == '\0')
                        {
                            string command = memory.ReadString((ulong)(cmd_lists + i * 0x7f0 + x), 128, false);
                            Console.WriteLine(command);
                        }

                        previus = memory.Read<char>((ulong)(cmd_lists + i * 0x7f0 + x));
                    }
                }
            }
        }
