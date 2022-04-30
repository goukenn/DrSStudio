

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CsvPreviewHandler.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CsvPreviewHandler.cs
*/
// Stephen Toub
using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;
namespace IGK.PreviewHandlerLib.PreviewHandlers
{
    using IGK.ICore;using IGK.PreviewHandlerLib.WinUI;
    [PreviewHandler("DEMO CSV Preview Handler", ".csv", "{8CF7761A-E923-470c-926E-8440C06FA8FE}")]
    [ProgId("MsdnMag.CsvPreviewHandler")]
    [Guid("47F228F7-5338-4bb4-AF7B-7B52278E1095")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public sealed class CsvPreviewHandler : StreamPreviewHandlerBase
    {
        protected override PreviewHandlerControl CreatePreviewHandlerControl()
        {
            return new CsvPreviewHandlerControl();
        }
        private sealed class CsvPreviewHandlerControl : StreamBasePreviewControl
        {
            public override void Load(Stream stream)
            {
                DataGridView grid = new DataGridView();
                grid.DataSource = ParseCsv(stream);
                grid.ReadOnly = true;
                grid.Dock = DockStyle.Fill;
                grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                Controls.Add(grid);
            }
            private static DataTable ParseCsv(Stream stream)
            {
                DataTable table = new DataTable();
                table.Locale = CultureInfo.CurrentCulture;
                table.TableName = stream is FileStream ? ((FileStream)stream).Name : "CSV";
                List<string[]> lines = new List<string[]>();
                int maxFields = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        line = line.Trim();
                        if (line.Length > 0)
                        {
                            string[] parts = line.Split(',');
                            maxFields = Math.Max(maxFields, parts.Length);
                            lines.Add(parts);
                        }
                    }
                }
                if (lines.Count > 0 && maxFields > 0)
                {
                    for (int i = 0; i < maxFields; i++) table.Columns.Add("Column " + i);
                    foreach (object[] line in lines)
                    {
                        table.Rows.Add(line);
                    }
                }
                return table;
            }
        }
    }
}

