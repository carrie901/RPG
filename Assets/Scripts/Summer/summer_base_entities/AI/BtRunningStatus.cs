namespace Summer.AI
{
    public class BtRunningStatus 
    {

        #region 运行状态

        public const int EXECUTING = 0;
        public const int FINISHED = 1;
        public const int TRANSITION = 2;

        #endregion

        //-------------------------------------------------------
        //User running status
        //100-999, reserved user executing status
        public const int USER_EXECUTING = 100;
        //>=1000, reserved user finished status
        public const int USER_FINISHED = 1000;
        //-------------------------------------------------------

        #region

        static public bool IsOk(int running_status)
        {
            return running_status == FINISHED ||
                   running_status >= USER_FINISHED;
        }
        static public bool IsError(int running_status)
        {
            return running_status < 0;
        }
        static public bool IsFinished(int running_status)
        {
            return IsOk(running_status) || IsError(running_status);
        }
        static public bool IsExecuting(int running_status)
        {
            return !IsFinished(running_status);
        }

        #endregion
    }
}

