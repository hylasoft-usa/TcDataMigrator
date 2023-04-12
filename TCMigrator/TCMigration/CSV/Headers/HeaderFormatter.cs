﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.Database;

namespace TCDataUtilities.CSV.Headers
{
    public class HeaderFormatter
    {
        IDbConnection connection;
        string separator;
        HeaderFormatter(IDbConnection connection, string separator = "__")
        {
            this.connection = connection;
            this.separator = separator;
        }
        public List<string> AutogenerateBasicHeaderRow(string tableName)
        {
            var columns = connection.getTableColumns(tableName);
            return AutogenerateBasicHeaderRow(columns);
        }
        public List<string> AutogenerateBasicHeaderRow(List<string> columnNames)
        {
            var headers = new List<String>();
            foreach (String s in columnNames)
            {
                if (s.Contains(separator))
                {
                    var parts = s.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                    var itemType = getItemType(parts[0]);
                    if (!String.IsNullOrWhiteSpace(itemType))
                    {
                        headers.Add(String.Format("{0}:{1}", itemType, connection.getCaseSensitiveHeaderValue(itemType, parts[1])));
                    }
                    else { headers.Add(s); }
                }
                else { headers.Add(s); }
            }
            return headers;
        }
        public DatasetHeader AutogenerateDatasetHeader(string parentCol, string datasetType, string datasetON, string relationType, string fileName, string originalFN, string volTag, string sdPath, string RevisionCol = "")
        {
            var p = splitCol(parentCol);
            var dst = splitCol(datasetType);
            var dsON = splitCol(datasetON);
            var rt = splitCol(relationType);
            var fn = splitCol(fileName);
            var ofn = splitCol(originalFN);
            var vol = splitCol(volTag);
            var sdp = splitCol(sdPath);
            DatasetHeader dsh = null;
            if (!String.IsNullOrWhiteSpace(RevisionCol))
            {
                HeaderSpecification rev = splitCol(RevisionCol);
                dsh = new DatasetHeader(p, dst, dsON, fn, ofn, vol, sdp, rt, rev);
            }
            else {
                var dsH = new DatasetHeader(p, dst, dsON, fn, ofn, vol, sdp, rt);
            }
            return dsh;

        }
        public List<string> AutogenerateBomHeader(string Parent, string Child, string Sequence, string Qty, string UOM, string parentRev = "", string childRev = "")
        {
            var p = splitCol(Parent);
            var c = splitCol(Child);
            var s = splitCol(Sequence);
            var qty = splitCol(Qty);
            var uom = splitCol(UOM);
            BOMHeader header = null;
            if (!string.IsNullOrWhiteSpace(parentRev))
            {
                var pRev = splitCol(parentRev);
                if (!string.IsNullOrWhiteSpace(childRev))
                {
                    var cRev = splitCol(childRev);
                    header = new BOMHeader(p, c, s, qty, uom, pRev, cRev);
                }
                else
                {
                    header = new BOMHeader(p, c, qty, uom, pRev);
                }
            } else if (String.IsNullOrWhiteSpace(parentRev) && !String.IsNullOrWhiteSpace(childRev))
            {
                var cRev = splitCol(childRev);
                header = new BOMHeader(p, c, s, qty, uom, null, cRev);
            }
            else
            {
                header = new BOMHeader(p, c, s, qty, uom);
            }
            return header.Header;

        }
        public List<string> AutogenerateRelationHeader(String parent, string child, string relationType, string parentRev = "",string childRev="")
        {
            var p = splitCol(parent);
            var c = splitCol(child);
            var rt = splitCol(relationType);
            RelationHeader rh = null;
            if (!string.IsNullOrEmpty(parentRev))
            {
                var pRev = splitCol(parentRev);
                if (!string.IsNullOrEmpty(childRev))
                {
                    var cRev = splitCol(childRev);
                    rh = new RelationHeader(p, c, rt, pRev, cRev);
                }
                else
                {
                    rh = new RelationHeader(p, c, rt, pRev);
                }
            }else if(String.IsNullOrEmpty(parentRev) && !String.IsNullOrEmpty(childRev))
            {
                var cRev = splitCol(childRev);
                rh = new RelationHeader(p, c, rt, null, cRev);
            }
            else
            {
                rh = new RelationHeader(p, c, rt);
            }
            return rh.Header;
        }
        private HeaderSpecification splitCol(string columnName)
        {
            if (columnName.Contains(separator))
            {
                var parts = columnName.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
                var it = getItemType(parts[0]);
                var headerValue = connection.getCaseSensitiveHeaderValue(it,parts[1]);
                return new HeaderSpecification(columnName, it, headerValue);
            }
            else
            {
                return new HeaderSpecification(columnName, columnName);
            }
        }
        private string getItemType(string typeCode)
        {
            var mappings = connection.getMappings();
            var tc = typeCode.ToLower();
            if (mappings.ContainsKey(tc))
            {
                return mappings[tc];
            }
            return "";
        }
    }
}