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
        public static InventoryAssignment CreatAssignment(int itemID,int itemAmount,bool  isTaken,string name,string description)
        {
            var assignment = CreateInstance<InventoryAssignment>();
            assignment.mItemID = itemID;
            assignment.mItemAmount = itemAmount;
            assignment.mIsTaken = isTaken;
            assignment.Name = name;
            assignment.Description = description;
            assignment.Display = true;
            return assignment;
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
