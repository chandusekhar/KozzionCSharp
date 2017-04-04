using System;
using System.Collections.Generic;
using System.Numerics;
using KozzionCryptography.multiparty;

namespace KozzionCryptography.multiparty
{
    public class PokerPlayer
    {
        public const int DECKSIZE     = 20;
        public const int PLAYER_COUNT = 2;
        public const int FLOPSIZE     = 3;

        private int d_player_id;
        private IChannel[] d_player_channals;
        private  BarnettSmartVTMF_dlog d_vmtf;

        // private final IChannel d_channal;

        public PokerPlayer(
            int player_id,
            IChannel[] player_channals,
            BarnettSmartVTMF_dlog vmtf)
        {
            d_player_id = player_id;
            d_player_channals = player_channals;
            d_vmtf = vmtf;
        }

        public void Run()
        {
            // create TMCG and VTMF instances
            BarnettSmartVTMF_dlog vtmf = d_vmtf;
            if (!vtmf.CheckGroup())
            {
                throw new Exception("P_" + d_player_id + ": Group G was not correctly generated!");
            }
            System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done generating keys");

            // create and exchange VTMF keys
            vtmf.KeyGenerationProtocol_GenerateKey();
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (i != d_player_id)
                {
                    vtmf.KeyGenerationProtocol_PublishKey(d_player_channals[i]);
                }
            }
            for (int i = 0; i < PLAYER_COUNT; i++)
            {
                if (i != d_player_id)
                {
                    if (!vtmf.KeyGenerationProtocol_UpdateKey(d_player_channals[i]))
                    {
                        throw new Exception("P_" + d_player_id + ": Public key of P_" + i + " was not correctly generated!");
                    }
                }
            }
            //vtmf.KeyGenerationProtocol_Finalize();
            System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done create and exchange VTMF keys");

            // VSSHE
            GrothVSSHE vsshe;
            if (d_player_id == 0)
            {
                // create and publish VSSHE instance as leader
                vsshe = new GrothVSSHE(DECKSIZE, vtmf.p, vtmf.q, vtmf.k, vtmf.g, vtmf.h);
                System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done GrothVSSHE");
                if (!vsshe.CheckGroup())
                {
                    throw new Exception("P_" + d_player_id + ": VSSHE instance was not correctly generated!");
                }
               System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done CheckGroup");
                for (int i = 1; i < PLAYER_COUNT; i++)
                {
                    vsshe.PublishGroup(d_player_channals[i]); // TODO not also publish to self!
                }

                System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done VSSHE");

            }
            else
            {
                // receive and create VSSHE instance as non-leader
                vsshe = new GrothVSSHE(DECKSIZE, d_player_channals[0]);
                System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done GrothVSSHE");
                if (!vsshe.CheckGroup())
                {
                    throw new Exception("P_" + d_player_id + ": VSSHE instance was not correctly generated!");
                }
                if (vtmf.h != vsshe.com.h)
                {
                    throw new Exception("P_" + d_player_id + ": VSSHE: Common public key does not match! " + vtmf.h.BitLength() + " " + vsshe.com.h.BitLength());
                }
                if (vtmf.q != vsshe.com.q)
                {
                    throw new Exception("P_" + d_player_id + ": VSSHE: VSSHE: Subgroup order does not match!");
                }
                if (vtmf.p !=vsshe.p  || vtmf.q!=vsshe.q || vtmf.g!=vsshe.g || vtmf.h!=vsshe.h)
                {
                    throw new Exception("P_" + d_player_id + ": VSSHE: Encryption scheme does not match!");
                }
            
                System.Diagnostics.Debug.WriteLine("P_" + d_player_id + " done VSSHE");
            }
                    
        }
    }
}
