using Service.Contracts.ViewModels;
using Service.Contracts.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.Entities.Models.Mapper
{    
    public class OrderBatchItemRelator
    {
        private BS_Order_SalesOrder_Batch current;
        public BS_Order_SalesOrder_Batch MapIt(BS_Order_SalesOrder_Batch a, BS_Order_SalesOrder_BatchItem p)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (a == null)
                return current;

            // Is this the same author as the current one we're processing
            if (current != null && current.sBatchCode == a.sBatchCode)
            {
                // Yes, just add this post to the current author's collection of posts
                current.BS_Order_SalesOrder_BatchItem.Add(p);

                // Return null to indicate we're not done with this author yet
                return null;
            }

            // This is a different author to the current one, or this is the 
            // first time through and we don't have an author yet

            // Save the current author
            var prev = current;

            // Setup the new current author
            current = a;
            current.BS_Order_SalesOrder_BatchItem = new List<BS_Order_SalesOrder_BatchItem>();
            current.BS_Order_SalesOrder_BatchItem.Add(p);

            // Return the now populated previous author (or null if first time through)
            return prev;
        }
    }
    public class OrderBatchItemDtoRelator
    {
        private BS_Order_SalesOrder_BatchDto current;
        public BS_Order_SalesOrder_BatchDto MapIt(BS_Order_SalesOrder_BatchDto a, BS_Order_SalesOrder_BatchItemDto p)
        {
            // Terminating call.  Since we can return null from this function
            // we need to be ready for PetaPoco to callback later with null
            // parameters
            if (a == null)
                return current;

            // Is this the same author as the current one we're processing
            if (current != null && current.sBatchCode == a.sBatchCode)
            {
                // Yes, just add this post to the current author's collection of posts
                current.BS_Order_SalesOrder_BatchItem.Add(p);

                // Return null to indicate we're not done with this author yet
                return null;
            }

            // This is a different author to the current one, or this is the 
            // first time through and we don't have an author yet

            // Save the current author
            var prev = current;

            // Setup the new current author
            current = a;
            current.BS_Order_SalesOrder_BatchItem = new List<BS_Order_SalesOrder_BatchItemDto>();
            current.BS_Order_SalesOrder_BatchItem.Add(p);

            // Return the now populated previous author (or null if first time through)
            return prev;
        }
    }
}