using System;
using System.Numerics;
using System.Threading;
using KozzionCryptography.multiparty;
using Microsoft.VisualStudio.TestTools.UnitTesting;

 
       
namespace KozzionCryptography.multiparty
{
	[TestClass]
	public class PokerPlayerTest
	{
		[TestMethod]
		public void MentalPokerTest() 
		{
			// create and check VTMF instance
			System.Diagnostics.Debug.WriteLine("BarnettSmartVTMF_dlog()");
			BarnettSmartVTMF_dlog vtmf = new BarnettSmartVTMF_dlog();
			System.Diagnostics.Debug.WriteLine("vtmf.CheckGroup()");

			Assert.IsTrue(vtmf.CheckGroup()); // TODO random fail??

			// publish VTMF instance as string stream
			System.Diagnostics.Debug.WriteLine("vtmf.PublishGroup(vtmf_str)");
			//vtmf.PublishGroup(d_channal);
			int player_count = PokerPlayer.PLAYER_COUNT;

			// open channel
			IChannel [][] channals = new IChannel[player_count][];
            for (int index_player = 0; index_player < PokerPlayer.PLAYER_COUNT; index_player++)
            {
                channals[index_player] = new IChannel[player_count];
            }

			for (int index_send = 0; index_send < PokerPlayer.PLAYER_COUNT; index_send++)
			{
			
				for (int index_recieve = index_send + 1; index_recieve < PokerPlayer.PLAYER_COUNT; index_recieve++)
				{
					ChannelTwoWayBigInteger channel = new ChannelTwoWayBigInteger();
					channals[index_send][index_recieve] = channel.Channel0;
					channals[index_recieve][index_send] = channel.Channel1;
				}
			}
			System.Diagnostics.Debug.WriteLine("starting " + PokerPlayer.PLAYER_COUNT + " players");
			// start poker childs
        
			Thread [] player_treads = new Thread [PokerPlayer.PLAYER_COUNT];
			for (int i = 0; i < PokerPlayer.PLAYER_COUNT; i++)
			{
				PokerPlayer poker_player = new PokerPlayer(i,channals[i], new BarnettSmartVTMF_dlog(vtmf));
				player_treads[i] = new Thread(poker_player.Run);
				player_treads[i].Start();
			}
        
			for (int i = 0; i < PokerPlayer.PLAYER_COUNT; i++)
			{
				player_treads[i].Join();
			} 
		}
	}
}
