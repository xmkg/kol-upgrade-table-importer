/**
 * ______________________________________________________
 * This file is part of ko-item-tbl-importer project.
 * 
 * @author       Mustafa Kemal Gılor <mustafagilor@gmail.com> (2015)
 * .
 * SPDX-License-Identifier:	MIT
 * ______________________________________________________
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace KOUpgradeEditor
{
    public class AlphanumComparatorFast : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string s1 = x as string;
            if (s1 == null)
            {
                return 0;
            }
            string s2 = y as string;
            if (s2 == null)
            {
                return 0;
            }

            int len1 = s1.Length;
            int len2 = s2.Length;
            int marker1 = 0;
            int marker2 = 0;

            // Walk through two the strings with two markers.
            while (marker1 < len1 && marker2 < len2)
            {
                char ch1 = s1[marker1];
                char ch2 = s2[marker2];

                // Some buffers we can build up characters in for each chunk.
                char[] space1 = new char[len1];
                int loc1 = 0;
                char[] space2 = new char[len2];
                int loc2 = 0;

                // Walk through all following characters that are digits or
                // characters in BOTH strings starting at the appropriate marker.
                // Collect char arrays.
                do
                {
                    space1[loc1++] = ch1;
                    marker1++;

                    if (marker1 < len1)
                    {
                        ch1 = s1[marker1];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                do
                {
                    space2[loc2++] = ch2;
                    marker2++;

                    if (marker2 < len2)
                    {
                        ch2 = s2[marker2];
                    }
                    else
                    {
                        break;
                    }
                } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                // If we have collected numbers, compare them numerically.
                // Otherwise, if we have strings, compare them alphabetically.
                string str1 = new string(space1);
                string str2 = new string(space2);

                int result;

                if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                {
                    int thisNumericChunk = int.Parse(str1);
                    int thatNumericChunk = int.Parse(str2);
                    result = thisNumericChunk.CompareTo(thatNumericChunk);
                }
                else
                {
                    result = str1.CompareTo(str2);
                }

                if (result != 0)
                {
                    return result;
                }
            }
            return len1 - len2;
        }
    }
    class ItemInfo
    {
        public int ID;
        public string Name;
    }

    internal enum ItemType
    {
        ITEM_TYPE_NORMAL = 0,
        ITEM_TYPE_MAGIC = 1,
        ITEM_TYPE_RARE = 2,
        ITEM_TYPE_CRAFT = 3,
        ITEM_TYPE_UNIQUE = 4,
        ITEM_TYPE_UPGRADE = 5,
        ITEM_TYPE_EVENT = 6,
        ITEM_TYPE_COSPRE = 8,
        ITEM_TYPE_REVERSE = 11,
        ITEM_TYPE_UNIQUE_REVERSE = 12

    };

    static class StaticReference
    {
        public static readonly DataSet TableSet = new DataSet();
        private const string DataPath = @"./Data/";

        #region Encryption
        internal class EncryptionKOStandard
        {
            internal void Decode(ref byte[] data)
            {
                uint num = 0x816;
                for (var i = 0; i < data.Length; i++)
                {
                    var num3 = data[i];
                    var num4 = num;
                    byte num5 = 0;
                    num4 &= 0xff00;
                    num4 = num4 >> 8;
                    num5 = (byte)(num4 ^ num3);
                    num4 = num3;
                    num4 += num;
                    num4 &= 0xffff;
                    num4 *= 0x6081;
                    num4 &= 0xffff;
                    num4 += 0x1608;
                    num4 &= 0xffff;
                    num = num4;
                    data[i] = num5;
                }
            }

            public void Encode(FileStream stream)
            {
                var num = stream.ReadByte();
                uint num2 = 0x816;
                while (num != -1)
                {
                    stream.Seek(-1L, SeekOrigin.Current);
                    var num3 = (byte)(num & 0xff);
                    byte num4 = 0;
                    var num5 = num2;
                    num5 &= 0xff00;
                    num5 = num5 >> 8;
                    num4 = (byte)(num5 ^ num3);
                    num5 = num4;
                    num5 += num2;
                    num5 &= 0xffff;
                    num5 *= 0x6081;
                    num5 &= 0xffff;
                    num5 += 0x1608;
                    num5 &= 0xffff;
                    num2 = num5;
                    stream.WriteByte(num4);
                    num = stream.ReadByte();
                }
            }

        }
        #endregion
        #region Load Table
        public static bool LoadTable(string fname)
        {
            var anyError = false;

            if (File.Exists(DataPath + fname))
            {
                LoadByteDataIntoView(LoadAndDecodeFile(DataPath + fname), fname);
                Trace.TraceInformation(fname + " loaded");
            }
            else
            {
                anyError = true;
            }


            return anyError;
        }


        private static byte[] LoadAndDecodeFile(string fileName)
        {
            var encDec = new EncryptionKOStandard();

            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                int offset = 0;
                var buffer = new byte[stream.Length];
                while (offset < stream.Length)
                {
                    offset += stream.Read(buffer, offset, ((int)stream.Length) - offset);
                }
                stream.Close();
                encDec.Decode(ref buffer);
                return buffer;
            }
        }


        private static void LoadByteDataIntoView(byte[] fileData, string Name)
        {
            int startIndex = 0;
            int num2 = BitConverter.ToInt32(fileData, startIndex);
            string tablename = Name;
            startIndex += 4;
            var numArray = new int[num2];
            var table = new DataTable(tablename);
            for (int i = 0; i < num2; i++)
            {
                DataColumn column;
                int num4 = BitConverter.ToInt32(fileData, startIndex);
                numArray[i] = num4;
                string prefix = i.ToString(CultureInfo.InvariantCulture);
                switch (num4)
                {
                    case 1:
                        column = new DataColumn(prefix + "\n(Signed Byte)", typeof(sbyte))
                        {
                            DefaultValue = (sbyte)0
                        };
                        break;

                    case 2:
                        column = new DataColumn(prefix + "\n(Byte)", typeof(byte))
                        {
                            DefaultValue = (byte)0
                        };
                        break;

                    case 3:
                        column = new DataColumn(prefix + "\n(Int16)", typeof(short))
                        {
                            DefaultValue = (short)0
                        };
                        break;

                    case 5:
                        column = new DataColumn(prefix + "\n(Int32)", typeof(int))
                        {
                            DefaultValue = 0
                        };
                        break;

                    case 6:
                        column = new DataColumn(prefix + "\n(UInt32)", typeof(uint))
                        {
                            DefaultValue = 0
                        };
                        break;

                    case 7:
                        column = new DataColumn(prefix + "\n(String)", typeof(string))
                        {
                            DefaultValue = ""
                        };
                        break;

                    case 8:
                        column = new DataColumn(prefix + "\n(Float)", typeof(float))
                        {
                            DefaultValue = 0f
                        };
                        break;

                    default:
                        column = new DataColumn(prefix + "\n(Unknown) " + num4.ToString(CultureInfo.InvariantCulture))
                        {
                            DefaultValue = 0
                        };
                        break;
                }
                table.Columns.Add(column);
                startIndex += 4;
            }

            int num5 = BitConverter.ToInt32(fileData, startIndex);
            startIndex += 4;
            for (int j = 0; (j < num5) && (startIndex < fileData.Length); j++)
            {
                DataRow row = table.NewRow();
                for (int k = 0; (k < num2) && (startIndex < fileData.Length); k++)
                {
                    int num8;
                    switch (numArray[k])
                    {
                        case 1:
                            {
                                row[k] = (fileData[startIndex] > 0x7f)
                                    ? (fileData[startIndex] - 0x100)
                                    : fileData[startIndex];
                                startIndex++;
                                continue;
                            }
                        case 2:
                            {
                                row[k] = fileData[startIndex];
                                startIndex++;
                                continue;
                            }
                        case 3:
                            {
                                row[k] = BitConverter.ToInt16(fileData, startIndex);
                                startIndex += 2;
                                continue;
                            }
                        case 5:
                            {
                                row[k] = BitConverter.ToInt32(fileData, startIndex);
                                startIndex += 4;
                                continue;
                            }
                        case 6:
                            {
                                row[k] = BitConverter.ToUInt32(fileData, startIndex);
                                startIndex += 4;
                                continue;
                            }
                        case 7:
                            {
                                num8 = BitConverter.ToInt32(fileData, startIndex);
                                startIndex += 4;
                                if (num8 > 0)
                                {
                                    break;
                                }
                                continue;
                            }
                        case 8:
                            {
                                row[k] = BitConverter.ToSingle(fileData, startIndex);
                                startIndex += 4;
                                continue;
                            }
                        default:
                            goto Label_03F5;
                    }
                    var chArray = new char[num8];
                    for (int m = 0; m < num8; m++)
                    {
                        chArray[m] = (char)fileData[startIndex];
                        startIndex++;
                    }
                    row[k] = new string(chArray);
                    continue;
                Label_03F5:
                    row[k] = BitConverter.ToInt32(fileData, startIndex);
                    startIndex += 4;
                }
                table.Rows.Add(row);
            }

            TableSet.Tables.Add(table);
        }

        #endregion


        /* NOTE : Only each sublist is sorted. */
        public static List<List<DataRow>> GenerateVariationListNS(byte extension, int itemid)
        {
            /* Ordered by ID */
            return GenerateVariationListN(extension, itemid).Select(list => new List<DataRow>(list.OrderBy(p => p[0]))).ToList(); 
        }
        /* NOTE : Only each sublist is sorted. */
        public static List<List<DataRow>> GenerateVariationListCS(byte extension, int itemid)
        {
            /* Ordered by ID */
            return GenerateVariationListC(extension,itemid).Select(list => new List<DataRow>(list.OrderBy(p => p[0]))).ToList();
        }
        /* 
         * Normal variation list with constant base.
         * (the item base does not change on each iteration.)
         * Entries are sorted alphanumerically first.
         * NOTE : The resulting variation list DOES NOT include the base item.
         */
        public static List<List<DataRow>> GenerateVariationListC(byte extension, int itemid)
        {
            #region Generate variations

            var variationList = new List<List<DataRow>>();
            var itemInfo = FetchItemInformation(itemid);
            if (itemInfo == null)
                return variationList;
            /* Fetch 'em all! */
            var extensionRows = GetExtensionRows(extension, itemid);

            /* If there's no extension exist, skip it directly.*/
            if (!extensionRows.Any())
                return variationList;


            var orderedExtensionRows = extensionRows.OrderBy(r => Convert.ToString(r[1]).ToLower(), new AlphanumComparatorFast());

            /* No extensions, no variations. */
            if (!orderedExtensionRows.Any())
                return variationList;

            {
                // Start of variation generation
                // This allows us to separate unique item(s) which are using
                // same base item (like lobo - lupus - lycaon) from each other.
                var baseIndex = 0;
                var currentBaseRow = orderedExtensionRows.ElementAt(baseIndex);
                DataRow prevBaseRow = null;
            restart_loop:
            //    Trace.Assert(currentBaseRow != prevBaseRow);
                var currentVariationRows = new List<DataRow>();
                 var baseName = Convert.ToString(currentBaseRow[1]).ToLowerInvariant();
                /* We can go no further with empty basename. */
                if (string.IsNullOrEmpty(baseName))
                    return variationList;
                baseName = baseName.Remove(baseName.Length - 4, 4);
            
                /* Start from current base index, iterate until end. */
                for (var i = baseIndex; i < orderedExtensionRows.Count(); i++)
                {
                    var currentRow = orderedExtensionRows.ElementAt(i);
                    var currentRowName = Convert.ToString(currentRow[1]).ToLowerInvariant();
                    /* Our variation must include base name
                     * Base : Hell Blood
                     * Variation : Hell Blood (+1)
                     * */
                    if (currentRowName.Contains(baseName))
                    {
                        currentVariationRows.Add(currentRow);
                        /* Check for last iteration */
                        if (i == orderedExtensionRows.Count() - 1)
                            variationList.Add(currentVariationRows);
                    }
                    else
                    {
                        variationList.Add(currentVariationRows);
                        prevBaseRow = currentBaseRow;
                        currentBaseRow = currentRow;
                        baseIndex = i;
                        goto restart_loop;
                    }
                }
            }
            return variationList;
            #endregion
        }

        /* 
          * Normal variation list 
          * (the item base automatically changes according to extension variation.)
          * Entries are sorted alphanumerically first.
          *  NOTE : The resulting variation list include the base item.
         * */
        public static List<List<DataRow>> GenerateVariationListN(byte extension, int itemid)
        {
            #region Generate variations
            var variationList = new List<List<DataRow>>();

            /* Fetch 'em all! */
            var extensionRows = GetExtensionRows(extension, itemid);

            /* If there's no extension exist, skip it directly.*/
            if (!extensionRows.Any())
                return variationList;


            var orderedExtensionRows = extensionRows.OrderBy(r => Convert.ToString(r[1]).ToLower(), new AlphanumComparatorFast());

            /* No extensions, no variations. */
            if (!orderedExtensionRows.Any())
                return variationList;

            {
                // Start of variation generation
                // This allows us to separate unique item(s) which are using
                // same base item (like lobo - lupus - lycaon) from each other.
                var baseIndex = 0;
                DataRow prevBaseRow = null;
                var currentBaseRow = orderedExtensionRows.ElementAt(baseIndex);
                restart_loop:
            //    Trace.Assert(currentBaseRow != prevBaseRow);
                var currentVariationRows = new List<DataRow>();
                var currentBaseName = Convert.ToString(currentBaseRow[1]).ToLowerInvariant();
                /* Start from current base index, iterate until end. */
                for (var i = baseIndex; i < orderedExtensionRows.Count(); i++)
                {
                    var currentRow = orderedExtensionRows.ElementAt(i);
                    var currentRowName = Convert.ToString(currentRow[1]).ToLowerInvariant();
                    /* Our variation must include base name
                     * Base : Hell Blood
                     * Variation : Hell Blood (+1)
                     * */
                    if (currentRowName.Contains(currentBaseName))
                    {
                        currentVariationRows.Add(currentRow);
                        /* Check for last iteration */
                        if (i == orderedExtensionRows.Count() - 1)
                            variationList.Add(currentVariationRows);
                    }
                    else
                    {
                        variationList.Add(currentVariationRows);
                        prevBaseRow = currentBaseRow;
                        currentBaseRow = currentRow;
                        baseIndex = i;
                        goto restart_loop;
                    }
                }
            }
            return variationList;
            #endregion
        }

        public static ItemInfo FetchItemInformation(int id)
        {
            return (from DataRow r in TableSet.Tables["item_org_us.tbl"].Rows let currentID = Convert.ToInt32(r[0]) where currentID == id select r).Select(r => new ItemInfo
            {
                ID = id, Name = Convert.ToString((object) r[2])
            }).FirstOrDefault();
        }

        public static DataRow GetReverseBaseVariant(string itemName)
        {
            foreach (DataRow row in TableSet.Tables["item_org_us.tbl"].Rows)
            {
                string name = Convert.ToString(row[2]).ToLowerInvariant();
                int ext = Convert.ToInt32(row[1]);
                if (ext < 24) 
                    continue;

                if (String.Compare(itemName.ToLowerInvariant(), name, StringComparison.Ordinal) == 0)
                    return row;
            }
            return null;
        }

        public static DataRow[] GetExtensionRows(byte index, int baseitem,bool enforceBase = true)
        {
            List<DataRow> drowlist = (from DataRow r in TableSet.Tables[String.Format("item_ext_{0}_us.tbl", index)].Rows
                                      where Convert.ToInt32(r[2]) == baseitem || (!enforceBase && Convert.ToInt32(r[2]) == 0)
                                      select r).ToList();
            return drowlist.ToArray();
        }
    }
}
