﻿// This code was generated by a tool. 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
namespace net.r_eg.Conari
{
    using System;

    // TODO: it can be as here - vssbe.r-eg.net/doc/Examples/Version
    // but currently I don't want to change this, because it will be automatically changed if plugin exists (use .version)
    public struct LunaRoadVersion
    {
        public static readonly Version number = new Version(S_NUM_REV);

        public const string S_NUM       = "0.0.1";
        public const string S_REV       = "0";
        public const string S_NUM_REV   = S_NUM + "." + S_REV;

        public const string BRANCH_NAME = "-";
        public const string BRANCH_SHA1 = "-";
        public const string BRANCH_REVC = "-";

        internal const string S_INFO        = S_NUM_REV + " [ " + BRANCH_SHA1 + " ]";
        internal const string S_INFO_FULL   = S_INFO + " /'" + BRANCH_NAME + "':" + BRANCH_REVC;
    }
}
