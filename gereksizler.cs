        /*
                // oyuncuların toplam ne kadar hit yaptığını gösterir
                Int64 m_pBulletServices = memory.Read<Int64>(csPlayerPawn + 0x16B0);
                Int64 m_totalHitsOnServer  = memory.Read<Int64>((ulong)(m_pBulletServices + 0x40));
                Console.WriteLine("value -> " + value);


                // local pozisyona çok yakın bir değer veriyor saçma
                Vector3 m_vecLastClipCameraForward = memory.Read<Vector3>(localplayer + 0x128C);
                Console.WriteLine("m_vecLastClipCameraForward: " + m_vecLastClipCameraForward);


                // bu bi değer veriyor ama anlamadım aq
                float m_flHitHeading = memory.Read<float>(csPlayerPawn + 0x1424);
                //Console.WriteLine("m_flHitHeading: " + m_flHitHeading);


                // dumperde veri yok aq
                //Console.WriteLine("name: " + name);
                if (name.Contains("grenade_projectile"))
                {
                    Console.WriteLine("name: " + name);
                }


                // chams denemesi yarrakla sonuçlandı oldhealt da 0 dönüyor hep
                if (name.Contains("basemodelentity"))
                {
                    Vector3 m_clrRender = memory.Read<Vector3>((ulong)(entController + 0xA73));
                    //Console.WriteLine("m_clrRender : " + m_clrRender);

                    int oldhealth = memory.Read<int>((ulong)(entController + 0xA6C));
                    Console.WriteLine("oldhealth : " + oldhealth);
                }


            // buna bakılacak ama olmaz gibi. QAngle.X 0 ve 2 arası değer veriyor.
            public static class C_CSGOViewModel // C_PredictedViewModel
            {
                public const int m_bShouldIgnoreOffsetAndAccuracy = 0xF10; // bool
                public const int m_nWeaponParity = 0xF14; // uint32_t
                public const int m_nOldWeaponParity = 0xF18; // uint32_t
                public const int m_nLastKnownAssociatedWeaponEntIndex = 0xF1C; // CEntityIndex
                public const int m_bNeedToQueueHighResComposite = 0xF20; // bool
                public const int m_vLoweredWeaponOffset = 0xF64; // QAngle
            }
                if (name.Contains("csgo_viewmodel"))
                {
                    //Console.WriteLine("name: " + name);

                    bool m_bShouldIgnoreOffsetAndAccuracy = memory.Read<bool>((ulong)(entController + C_CSGOViewModel.m_bShouldIgnoreOffsetAndAccuracy));
                    uint m_nWeaponParity = memory.Read<uint>((ulong)(entController + C_CSGOViewModel.m_nWeaponParity));
                    uint m_nOldWeaponParity = memory.Read<uint>((ulong)(entController + C_CSGOViewModel.m_nOldWeaponParity));
                    int m_nLastKnownAssociatedWeaponEntIndex = memory.Read<int>((ulong)(entController + C_CSGOViewModel.m_nLastKnownAssociatedWeaponEntIndex));
                    bool m_bNeedToQueueHighResComposite = memory.Read<bool>((ulong)(entController + C_CSGOViewModel.m_bNeedToQueueHighResComposite));
                    Vector3 m_vLoweredWeaponOffset = memory.Read<Vector3>((ulong)(entController + C_CSGOViewModel.m_vLoweredWeaponOffset));

                    Console.WriteLine("m_bShouldIgnoreOffsetAndAccuracy: " + m_bShouldIgnoreOffsetAndAccuracy);
                    Console.WriteLine("m_nWeaponParity: " + m_nWeaponParity);
                    Console.WriteLine("m_nOldWeaponParity: " + m_nOldWeaponParity);
                    Console.WriteLine("m_nLastKnownAssociatedWeaponEntIndex: " + m_nLastKnownAssociatedWeaponEntIndex);
                    Console.WriteLine("m_bNeedToQueueHighResComposite: " + m_bNeedToQueueHighResComposite);
                    Console.WriteLine("m_vLoweredWeaponOffset: " + m_vLoweredWeaponOffset);
                }


                    // 0 dönüyor
                    int spawntime = memory.Read<int>((ulong)(entController + 0x10A0));
                    Console.WriteLine("spawntime: " + spawntime);


                    // sanırım server time tick i döndürüyor
                    //Console.WriteLine(memory.Read<int>((ulong)(entController + 0x1130)));






                    // comment changer not working
                    UInt64 InventoryServices = memory.Read<UInt64>(localcontroller + 0x708);
                    int m_nPersonaDataPublicCommendsLeader = memory.Read<int>(InventoryServices + 0x64);
                    Console.WriteLine("m_nPersonaDataPublicCommendsLeader: " + m_nPersonaDataPublicCommendsLeader);

                    memory.Write<int>(InventoryServices + 0x64, 5);
                    


                // spawn tick gibi tamamen gereksiz:
                Int32 m_nVisibilityNoInterpolationTick = memory.Read<Int32>(csPlayerPawn + 0x340);
                Console.WriteLine("m_nVisibilityNoInterpolationTick : " + m_nVisibilityNoInterpolationTick);




                    UInt64 m_pMovementServices = memory.Read<UInt64>(localplayer + 0x10E8);
                    Vector3 m_vecForward = memory.Read<Vector3>(m_pMovementServices + 0x47C);
                    //Console.WriteLine("m_vecForward -> " + m_vecForward);

                    int tracecount = memory.Read<int>(m_pMovementServices + 0x468);
                    //Console.WriteLine("tracecount -> " + tracecount);



                    // muhtemelen input offseti tamamen bozuk
                    IntPtr input = memory.Read<IntPtr>(memory.client + 0x9078);
                    byte thirdperson = memory.Read<byte>((ulong)(input + 0x8E01));
                    Console.WriteLine("input -> " + input);
                    Console.WriteLine("thirdperson -> " + thirdperson);



        // glow & chams test

                        var color = memory.Read<Vector3>(player + 0xA83);
                        memory.Write<Vector3>(player + 0xA83, new Vector3(100, 155, 255));

                        var baseglow = memory.Read<Int64>((ulong)(listEntry + 0xBA8));
                        memory.Write<Vector3>((ulong)(baseglow + 0x8), new Vector3(100, 155, 255));
                        memory.Write<int>((ulong)(baseglow + 0x30), 1);
                        memory.Write<int>((ulong)(baseglow + 0x34), 2);
                        memory.Write<int>((ulong)baseglow + 0x51, true);



                if (namestr.Contains("SmokeGrenadeProjectile"))
                {
                    Vector3 m_vecExplodeEffectOrigin = memory.Read<Vector3>(entController + 0x10EC);
                    Console.WriteLine("m_vecExplodeEffectOrigin: " + m_vecExplodeEffectOrigin);

                    //Vector3 m_arrTrajectoryTrailPoints = memory.Read<Vector3>(entController + 0x1120);
                    //Console.WriteLine("m_arrTrajectoryTrailPoints: " + m_arrTrajectoryTrailPoints);

                    //Vector3 m_vInitialPosition = memory.Read<Vector3>(entController + 0x10C0);
                    //Vector3 m_vInitialVelocity = memory.Read<Vector3>(entController + 0x10CC);
                    //Vector3 result = m_vInitialPosition + m_vInitialVelocity;

                    //Console.WriteLine("m_vInitialPosition: " + m_vInitialPosition);
                    //Console.WriteLine("m_vInitialVelocity: " + m_vInitialVelocity);
                    //Console.WriteLine("result: " + result);

                    /*
                        public const nint m_vInitialPosition = 0x10C0; // Vector
                        public const nint m_vInitialVelocity = 0x10CC; // Vector
                    
    }

        //blood color in C_BaseCombatCharacter 
        //memory.Write<int>(csPlayerPawn + 0x1088, 255);

        */
