using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1_CMMT
{

    public struct Frame_Info //产品的排列信息
    {
        public int ImageXNum;
        public int ImageYNum;
        public int FrameXNum;
        public int FrameYNum;
    }

    class Common
    {
    }


   enum MachineState
    {
        Run,
        Free
    }

    enum PauseState
    {
        Run,
        Pause
    }

    enum WorkState
    {
        Work,
        Free
    }

    enum UserLevel
    {
        User,
        Admin,
        SuperAdmin
    }


}
