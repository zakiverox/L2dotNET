﻿using L2dotNET.GameService.Model.Inventory;
using L2dotNET.GameService.Model.Items;
using L2dotNET.GameService.Model.Player;
using L2dotNET.GameService.Network.Serverpackets;
using L2dotNET.Network;

namespace L2dotNET.GameService.Network.Clientpackets
{
    class RequestUseItem : PacketBase
    {
        private readonly GameClient _client;
        private readonly int _sId;

        public RequestUseItem(Packet packet, GameClient client)
        {
            _client = client;
            _sId = packet.ReadInt();
        }

        public override void RunImpl()
        {
            L2Player player = _client.CurrentPlayer;

            if (player.PBlockAct == 1)
            {
                player.SendActionFailed();
                return;
            }

            L2Item item = player.GetItemByObjId(_sId);

            if (item == null)
            {
                player.SendSystemMessage(SystemMessage.SystemMessageId.IncorrectItem);
                return;
            }

            if (player.TradeState != 0)
            {
                player.SendSystemMessage(SystemMessage.SystemMessageId.CannotPickupOrUseItemWhileTrading);
                player.SendActionFailed();
                return;
            }
            
            if (!item.Equipped)
            {
                player.Inventory.Paperdoll[Inventory.GetPaperdollIndex(item.Template.BodyPart)] = item;
                item.Location = L2Item.ItemLocation.Paperdoll;
                item.SlotLocation = Inventory.GetPaperdollIndex(item.Template.BodyPart);
                item.PaperdollSlot = Inventory.GetPaperdollIndex(item.Template.BodyPart);
                item.IsEquipped = 1;
                player.BroadcastUserInfo();
                player.SendPacket(new ItemList(player,true));
                return;
            }

            if (ItemHandler.Instance.Process(player, item))
                return;

            //switch (item.Template.DefaultAction)
            //{
            //    case "action_capsule":
            //        Capsule.Instance.Process(player, item);
            //        break;
            //    case "action_call_skill":
            //    {
            //        Skill skill = item.Template.ItemSkill;
            //        if (skill != null)
            //            player.AddEffect(player, skill, true, false);
            //        else
            //            player.SendMessage("skill onCall was not found.");
            //    }
            //        break;
            //}
        }
    }
}