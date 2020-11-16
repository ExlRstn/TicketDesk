using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TicketDesk.Web.Client.Infrastructure.Helpers
{
    /// <summary>
    /// Create string table
    /// </summary>
    public class Table : IDisposable
    {
        private StringBuilder _sb;

        public Table(StringBuilder sb, string id = "default", string classValue = "")
        {
            _sb = sb;
            _sb.Append($"<table id=\"{id}\" style=\"border-collapse: collapse;width: 100%;\">\n");
        }

        public void Dispose()
        {
            _sb.Append("</table>");
        }

        public Row AddRow()
        {
            return new Row(_sb);
        }

        public Row AddHeaderRow()
        {
            return new Row(_sb, true);
        }

        public void StartTableBody()
        {
            _sb.Append("<tbody>");

        }

        public void EndTableBody()
        {
            _sb.Append("</tbody>");

        }
    }

    public class Row : IDisposable
    {
        private StringBuilder _sb;
        private bool _isHeader;
        public Row(StringBuilder sb, bool isHeader = false)
        {
            _sb = sb;
            _isHeader = isHeader;
            if (_isHeader)
            {
                _sb.Append("<thead style=\"padding: 8px;text-align: left;border-bottom: 1px solid #ddd; font-weight: bold;\">\n");
            }
            _sb.Append("\t<tr>\n");
        }

        public void Dispose()
        {
            _sb.Append("\t</tr style=\"border: 1px solid black;\">\n");
            if (_isHeader) 
            {
                _sb.Append("</thead>\n");
            }
        }

        public void AddCell(string innerText)
        {
            _sb.Append("\t\t<td style=\"padding: 8px;text-align: left;border-bottom: 1px solid #ddd;\">\n");
            _sb.Append("\t\t\t" + innerText);
            _sb.Append("\t\t</td>\n");
        }

        public void AddCellWithSpanValue(string innerText, int spanValue = 0)
        {
            _sb.Append($"\t\t<td colspan = \"{spanValue}\" style=\"padding: 8px;text-align: left;border-bottom: 1px solid #ddd;\">\n");
            _sb.Append("\t\t\t" + innerText);
            _sb.Append("\t\t</td>\n");
        }
    }
}
