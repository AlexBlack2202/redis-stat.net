// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayPrinter.cs" company="">
//   
// </copyright>
// <summary>
//   The array printer.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace redis_stat.net.console.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>The array printer.</summary>
    public static class ArrayPrinter
    {
        #region Constants

        /// <summary>The botto m_ joint.</summary>
        private const string BOTTOM_JOINT = "┴";

        /// <summary>The botto m_ lef t_ joint.</summary>
        private const string BOTTOM_LEFT_JOINT = "└";

        /// <summary>The botto m_ righ t_ joint.</summary>
        private const string BOTTOM_RIGHT_JOINT = "┘";

        /// <summary>The horizonta l_ line.</summary>
        private const char HORIZONTAL_LINE = '─';

        /// <summary>The joint.</summary>
        private const string JOINT = "┼";

        /// <summary>The lef t_ joint.</summary>
        private const string LEFT_JOINT = "├";

        /// <summary>The padding.</summary>
        private const char PADDING = ' ';

        /// <summary>The righ t_ joint.</summary>
        private const string RIGHT_JOINT = "┤";

        /// <summary>The to p_ joint.</summary>
        private const string TOP_JOINT = "┬";

        /// <summary>The to p_ lef t_ joint.</summary>
        private const string TOP_LEFT_JOINT = "┌";

        /// <summary>The to p_ righ t_ joint.</summary>
        private const string TOP_RIGHT_JOINT = "┐";

        /// <summary>The vertica l_ line.</summary>
        private const string VERTICAL_LINE = "│";

        #endregion

        #region Public Methods and Operators

        /// <summary>The get data in table format.</summary>
        /// <param name="table">The table.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetDataInTableFormat(this List<string[]> table)
        {
            var formattedTable = new StringBuilder();
            Array nextRow = table.FirstOrDefault();
            Array previousRow = table.FirstOrDefault();

            if (table == null || nextRow == null)
            {
                return string.Empty;
            }

            // FIRST LINE:
            int[] maximumCellWidths = GetMaxCellWidths(table);
            for (int i = 0; i < nextRow.Length; i++)
            {
                if (i == 0 && i == nextRow.Length - 1)
                {
                    formattedTable.Append(string.Format("{0}{1}{2}", TOP_LEFT_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), TOP_RIGHT_JOINT));
                }
                else if (i == 0)
                {
                    formattedTable.Append(string.Format("{0}{1}", TOP_LEFT_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                }
                else if (i == nextRow.Length - 1)
                {
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", TOP_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), TOP_RIGHT_JOINT));
                }
                else
                {
                    formattedTable.Append(string.Format("{0}{1}", TOP_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                }
            }

            int rowIndex = 0;
            int lastRowIndex = table.Count - 1;
            foreach (Array thisRow in table)
            {
                // LINE WITH VALUES:
                int cellIndex = 0;
                int lastCellIndex = thisRow.Length - 1;
                foreach (object thisCell in thisRow)
                {
                    string thisValue = thisCell.ToString().PadLeft(maximumCellWidths[cellIndex], PADDING);

                    if (cellIndex == 0 && cellIndex == lastCellIndex)
                    {
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", VERTICAL_LINE, thisValue, VERTICAL_LINE));
                    }
                    else if (cellIndex == 0)
                    {
                        formattedTable.Append(string.Format("{0}{1}", VERTICAL_LINE, thisValue));
                    }
                    else if (cellIndex == lastCellIndex)
                    {
                        formattedTable.AppendLine(string.Format("{0}{1}{2}", VERTICAL_LINE, thisValue, VERTICAL_LINE));
                    }
                    else
                    {
                        formattedTable.Append(string.Format("{0}{1}", VERTICAL_LINE, thisValue));
                    }

                    cellIndex++;
                }

                previousRow = thisRow;

                // SEPARATING LINE:
                if (rowIndex != lastRowIndex)
                {
                    nextRow = table[rowIndex + 1];

                    int maximumCells = Math.Max(previousRow.Length, nextRow.Length);
                    for (int i = 0; i < maximumCells; i++)
                    {
                        if (i == 0 && i == maximumCells - 1)
                        {
                            formattedTable.Append(string.Format("{0}{1}{2}", LEFT_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), RIGHT_JOINT));
                        }
                        else if (i == 0)
                        {
                            formattedTable.Append(string.Format("{0}{1}", LEFT_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                        }
                        else if (i == maximumCells - 1)
                        {
                            if (i > previousRow.Length)
                            {
                                formattedTable.AppendLine(string.Format("{0}{1}{2}", TOP_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), TOP_RIGHT_JOINT));
                            }
                            else if (i > nextRow.Length)
                            {
                                formattedTable.AppendLine(string.Format("{0}{1}{2}", BOTTOM_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), BOTTOM_RIGHT_JOINT));
                            }
                            else if (i > previousRow.Length - 1)
                            {
                                formattedTable.AppendLine(string.Format("{0}{1}{2}", JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), TOP_RIGHT_JOINT));
                            }
                            else if (i > nextRow.Length - 1)
                            {
                                formattedTable.AppendLine(string.Format("{0}{1}{2}", JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), BOTTOM_RIGHT_JOINT));
                            }
                            else
                            {
                                formattedTable.AppendLine(string.Format("{0}{1}{2}", JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), RIGHT_JOINT));
                            }
                        }
                        else
                        {
                            if (i > previousRow.Length)
                            {
                                formattedTable.Append(string.Format("{0}{1}", TOP_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                            }
                            else if (i > nextRow.Length)
                            {
                                formattedTable.Append(string.Format("{0}{1}", BOTTOM_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                            }
                            else
                            {
                                formattedTable.Append(string.Format("{0}{1}", JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                            }
                        }
                    }
                }

                rowIndex++;
            }

            // LAST LINE:
            for (int i = 0; i < previousRow.Length; i++)
            {
                if (i == 0)
                {
                    formattedTable.Append(string.Format("{0}{1}", BOTTOM_LEFT_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                }
                else if (i == previousRow.Length - 1)
                {
                    formattedTable.AppendLine(string.Format("{0}{1}{2}", BOTTOM_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE), BOTTOM_RIGHT_JOINT));
                }
                else
                {
                    formattedTable.Append(string.Format("{0}{1}", BOTTOM_JOINT, string.Empty.PadLeft(maximumCellWidths[i], HORIZONTAL_LINE)));
                }
            }

            return formattedTable.ToString();
        }

        #endregion

        #region Methods

        /// <summary>The get max cell widths.</summary>
        /// <param name="table">The table.</param>
        /// <returns>The <see cref="int[]"/>.</returns>
        private static int[] GetMaxCellWidths(List<string[]> table)
        {
            int maximumCells = (from Array row in table select row.Length).Concat(new[] { 0 }).Max();

            var maximumCellWidths = new int[maximumCells];
            for (int i = 0; i < maximumCellWidths.Length; i++)
            {
                maximumCellWidths[i] = 0;
            }

            foreach (Array row in table)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row.GetValue(i).ToString().Length > maximumCellWidths[i])
                    {
                        maximumCellWidths[i] = row.GetValue(i).ToString().Length;
                    }
                }
            }

            return maximumCellWidths;
        }

        #endregion
    }
}