﻿namespace SpriteHelper.Contract
{
    public enum SpecialMovement
    {
        //// must match SPECIAL_MOV_* consts

        None = 0,

        Stop60 = 1,
        Stop120 = 2,

        Clockwise = 5,
        CounterClockwise = 6,

        Sinus8 = 10,
        Sinus16 = 11,

        JumpBig = 16,
        JumpSmall = 17,
        JumpBigPause = 18,
        JumpSmallPause = 19,
    }
}
