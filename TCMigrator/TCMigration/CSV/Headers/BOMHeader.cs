using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDataUtilities.CSV.Headers
{
    public class BOMHeader
    {
        HeaderSpecification Parent;
        HeaderSpecification Child;
        HeaderSpecification ParentRev;
        HeaderSpecification ChildRev;
        HeaderSpecification SequenceNumber;
        HeaderSpecification Qty;
        HeaderSpecification Uom;
        Boolean useParentRev = false;
        Boolean useChildRev = false;
        readonly string ParentSpec = "{0}[Parent]:{1}";
        readonly string ChildSpec = "{0}[Child]:{1}";
        readonly string BasicSpec = "{0}:{1}";

        public List<String> Header {
            get
            {
                List<string> headers = new List<string>();
                headers.Add(String.Format(ParentSpec, Parent.ItemType, Parent.HeaderText));
                if (useParentRev)
                {
                    headers.Add(String.Format(ParentSpec, ParentRev.ItemType, ParentRev.HeaderText));
                }
                headers.Add(String.Format(ChildSpec, Child.ItemType, Child.HeaderText));
                if (useChildRev)
                {
                    headers.Add(String.Format(ChildSpec, ChildRev.ItemType, ChildRev.HeaderText));
                }
                headers.Add(String.Format(BasicSpec, SequenceNumber.ItemType, SequenceNumber.HeaderText));
                headers.Add(String.Format(BasicSpec, Qty.ItemType, Qty.HeaderText));
                headers.Add(String.Format(BasicSpec, Uom.ItemType, Uom.HeaderText));
                return headers;
            }
        }
        public BOMHeader(HeaderSpecification parent, HeaderSpecification child, HeaderSpecification seqNo, HeaderSpecification qty, HeaderSpecification uom, HeaderSpecification parentRev=null, HeaderSpecification childRev=null)
        {
            Parent = parent;
            Child = child;
            SequenceNumber = seqNo;
            Qty = qty;
            Uom = uom;
            if (ParentRev!=null)
            {
                useParentRev = true;
                ParentRev = parentRev;
            }
            if (ChildRev != null)
            {
                useChildRev = true;
                ChildRev = childRev;
            }
        }
    }
}
