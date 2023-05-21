using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Inventory;
using VGF.UI;

namespace VGF.Assignment
{
    public class InventoryAssignment : Assignment
{
        private int mItemID;
        private int mItemAmount;
        private bool mIsTaken;
        public static InventoryAssignment CreatAssignment(int itemID,int itemAmount,bool  isTaken)
        {
            var Assignment = CreateInstance<InventoryAssignment>();
            Assignment.mItemID = itemID;
            Assignment.mItemAmount = itemAmount;
            Assignment.mIsTaken = isTaken;
            return Assignment;
        }


        public override bool Check()
        {
            if (InventoryManager.Instance.SearchItem(mItemID, mItemAmount))
            {
                return true;
            }
            else return false;
        }

        public override void Finish()
        {
            if(mIsTaken)
            {
                InventoryManager.Instance.ReduceItem(mItemID, mItemAmount);
                string name = InventoryManager.Instance.GetItemDetails(mItemID).itemName;
                HintLoader.Instance.HintWithSeconds($"ƒ„ ß»•¡À{name}x{mItemAmount}", 2);
            }
        }

    }

}
