﻿namespace L2dotNET.Network.serverpackets
{
    class ExBasicActionList : GameserverPacket
    {
        private readonly int[] _defaultActionList;

        public ExBasicActionList()
        {
            const int count1 = 74; // 0 <-> (count1 - 1) //Update by rocknow
            const int count2 = 99; // 1000 <-> (1000 + count2 - 1) //Update by rocknow
            const int count3 = 16; // 5000 <-> (5000 + count3 - 1) //Update by rocknow
            int[] actionIds = new int[count1 + count2 + count3]; //Update by rocknow

            int index = 0;
            for (int i = 0; i < count1; i++)
                actionIds[index++] = i;
            for (int i = 0; i < count2; i++)
                actionIds[index++] = 1000 + i;
            for (int i = 0; i < count3; i++) //Update by rocknow
                actionIds[index++] = 5000 + i;

            _defaultActionList = actionIds;
        }

        public override void Write()
        {
            WriteByte(0xfe);
            WriteShort(0x5f);
            WriteInt(_defaultActionList.Length);
            foreach (int i in _defaultActionList)
                WriteInt(i);
        }
    }
}