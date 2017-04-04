namespace KozzionCryptography.multiparty
{
    public class Constants
    {

        public const int TMCG_MR_ITERATIONS = 64;
        public const long TMCG_GROTH_L_E = 80;
        public const long TMCG_DDH_SIZE = 1024;
        public const long TMCG_DLSE_SIZE = 160;
        public const long TMCG_KEYID_SIZE = 8;
        public const long TMCG_KEY_NIZK_STAGE1 = 16;
        public const long TMCG_KEY_NIZK_STAGE2 = 128;
        public const long TMCG_KEY_NIZK_STAGE3 = 128;
        public const int TMCG_MAX_CARDS = 128;
        public const long TMCG_MAX_TYPEBITS = 8;
        public const long TMCG_MPZ_IO_BASE = 36;
        public const long TMCG_PRAB_K0 = 20;
        public const long TMCG_QRA_SIZE = 1024;
        public const long TMCG_SAEP_S0 = 20;
        public const bool TMCG_HASH_COMMITMENT = true;
        public const int TMCG_MAX_FPOWM_T = 2048;
        public const long TMCG_MAX_PLAYERS = 32;
        public  const long TMCG_MAX_KEYBITS = ((TMCG_DDH_SIZE > TMCG_QRA_SIZE) ? TMCG_DDH_SIZE : TMCG_QRA_SIZE);
        public  const long TMCG_MAX_VALUE_CHARS = (TMCG_MAX_KEYBITS / 2L);
        public  const long TMCG_MAX_CARD_CHARS = TMCG_MAX_PLAYERS * TMCG_MAX_TYPEBITS * TMCG_MAX_VALUE_CHARS;
        public  const long TMCG_MAX_STACK_CHARS = (TMCG_MAX_CARDS * TMCG_MAX_CARD_CHARS);
        public  const long TMCG_MAX_KEY_CHARS = (TMCG_MAX_KEYBITS * 1024L);

    }
}