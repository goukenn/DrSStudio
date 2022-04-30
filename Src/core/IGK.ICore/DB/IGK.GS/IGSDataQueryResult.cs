using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    public interface  IGSDataQueryResult
    {
        bool Error { get; }
        string ErrorMessage { get; }
        /// <summary>
        /// get the rows collection
        /// </summary>
        IGSDataRowCollections Rows { get; }
        /// <summary>
        /// get the data column collection
        /// </summary>
        IGSDataColumnCollections Columns { get; }
        /// <summary>
        /// get the number of row in this query result
        /// </summary>
        int RowCount { get;  }
        /// <summary>
        /// get the number of the column
        /// </summary>
        int FieldCount { get; }
        /// <summary>
        /// get the query affected row
        /// </summary>
        int AffectedRow { get; }
        /// <summary>
        /// get the row at the index
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        IGSDataRow GetRowAt(int p);

        void SortBy(string p);
    }
}
