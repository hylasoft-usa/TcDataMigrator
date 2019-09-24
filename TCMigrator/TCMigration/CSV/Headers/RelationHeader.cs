using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDataUtilities.CSV.Headers
{
    public class RelationHeader
    {
        HeaderSpecification Parent;
        HeaderSpecification ParentRev;
        HeaderSpecification Child;
        HeaderSpecification ChildRev;
        HeaderSpecification Relation;
        bool useParentRev = false;
        bool useChildRev = false;
        readonly string ParentSpec = "{0}[1]:{1}";
        readonly string ChildSpec = "{0}[2]:{1}";
        readonly string RelationSpec = "{0}:{1}(primaryObject->{2}[1];secondary_object->{3}[2])";
        public List<string> Header { get
            {
                List<String> headers = new List<string>();
                headers.Add(String.Format(ParentSpec, Parent.ItemType, Parent.HeaderText));
                if (useParentRev)
                {
                    headers.Add(string.Format(ParentSpec, ParentRev.ItemType, Parent.HeaderText));
                }
                headers.Add(String.Format(ChildSpec, Child.ItemType, Child.HeaderText));
                if (useChildRev)
                {
                    headers.Add(string.Format(ChildSpec, ChildRev.ItemType, ChildRev.HeaderText));
                }
                if (useParentRev)
                {
                    if (useChildRev)
                    {
                        headers.Add(String.Format(RelationSpec, Relation.ItemType, Relation.HeaderText, ParentRev.ItemType, ChildRev.ItemType));
                    }
                    else { headers.Add(String.Format(RelationSpec, Relation.ItemType, Relation.HeaderText, ParentRev.ItemType, Child.ItemType)); }
                }
                else if(useChildRev && !useParentRev)
                {
                    headers.Add(String.Format(RelationSpec, Relation.ItemType, Relation.HeaderText, Parent.ItemType, ChildRev.ItemType));
                }
                else
                {
                    headers.Add(String.Format(RelationSpec, Relation.ItemType, Relation.HeaderText, Parent.ItemType, Child.ItemType));
                }
                return headers;
                
            }
        }

        public RelationHeader(HeaderSpecification parent, HeaderSpecification child, HeaderSpecification relation, HeaderSpecification parentRev=null, HeaderSpecification childRev = null)
        {
            Parent = parent;
            Child = child;
            Relation = relation;
            if (parentRev != null)
            {
                useParentRev = true;
                ParentRev = parentRev;
            }
            if (childRev != null)
            {
                useChildRev = true;
                ChildRev = childRev;
            }

        }
    }
}
