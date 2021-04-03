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
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace KOUpgradeEditor
{
    public partial class frmMain : Form
    {

        #region Declarations


      
     
        private Dictionary<int, UpgradeScroll> _scrolls = new Dictionary<int, UpgradeScroll>();

        private readonly Dictionary<int, UpgradeRow> _upgradeRows = new Dictionary<int, UpgradeRow>();
        private UpgradeScroll.GradeLevel _gradeLevel;
        private UpgradeScroll.UpgradeType _type;

     

        public static List<int> AccessorySlots = new List<int> { 10, 11, 12, 14 };

        private const int WEAPON_INDEX = 100000;
        private const int ARMOR_INDEX = 200000;
        private const int ACCESSORY_INDEX = 300000;
        private static int CURRENT_WEAPON_INDEX = 0;
        private static int CURRENT_ARMOR_INDEX = 0;
        private static int CURRENT_ACCESSORY_INDEX = 0;
        private const int TRINA_ID = 700002000;
        private const int KARIVIDIS_ID = 379258000;

        #endregion

        public frmMain()
        {
            InitializeComponent();
            /* Load tables */
            using (var l = new frmLoadTables())
                l.vLoad();
            
            LoadScrolls();
        }

        #region UI
        private void lbScrolls_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;

            var newIndex = lbScrolls.IndexFromPoint(e.X, e.Y);
            if (newIndex <= -1) return;

            lbScrolls.SelectedIndex = newIndex;
            cmsScrollList.Show(MousePosition);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddScroll();
        }

        private void AddScroll()
        {
            
            var id = int.Parse(txtScrollID.Text);

           // if(StaticReference.TableSet.Tables["item_org_us.tbl"])

            ItemInfo v = StaticReference.FetchItemInformation(id);
            if (v == null)
                return;
       

            if (_scrolls.ContainsKey(id))
                return;

            var s = new UpgradeScroll
            {
                ID = v.ID,
                Grade = UpgradeScroll.GradeLevel.BLESSED,
                Type = UpgradeScroll.UpgradeType.REGULAR,
                Name = v.Name,
                Rates = new List<Rate>(),
               
            };
            if (s.ID == 379257000) // reverse str.
            {
                for (var i = 0; i < 31; i++)
                    s.Rates.Add(new Rate {Cost = 0, Grade = i, Percent = 2000,TrinaPercent = 5000});
                s.Modifier = 1;
                s.Grade = UpgradeScroll.GradeLevel.REVERSE_STRENGTH;
                s.Type = UpgradeScroll.UpgradeType.REVERSE;
                
            }
            else if (s.ID == 379256000) // reverse conversion
            {
                s.Rates.Add(new Rate {Cost = 1200000,Grade = 0,Percent = 10000,TrinaPercent = 10000});
                s.Grade = UpgradeScroll.GradeLevel.REVERSE_CONVERSION;
                s.Type = UpgradeScroll.UpgradeType.REVERSE;
            }
            else
                for (var i = 0; i < 11; i++)
                    s.Rates.Add(new Rate {Cost = 0, Grade = i, Percent = 10000});

            _scrolls.Add(s.ID, s);
            lbScrolls.Items.Add(s);
        }

        private void lbGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbGrades.SelectedItem == null)
                return;

            var r = (Rate)lbGrades.SelectedItem;
            txtPercent.Text = r.Percent.ToString();
            txtCost.Text = r.Cost.ToString();
            txtModifier.Text = ((UpgradeScroll)lbScrolls.SelectedItem).Modifier.ToString();
            txtTrinaPercent.Text = ((UpgradeScroll)lbScrolls.SelectedItem).Rates[r.Grade].TrinaPercent.ToString();
        }

        private void lbScrolls_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbScrolls.SelectedItem == null)
                return;

            var s = (UpgradeScroll)lbScrolls.SelectedItem;

            lbGrades.Items.Clear();

            CheckGrade(s.Grade);
            CheckType(s.Type);

            cbAccessory.Checked = s.Accessory;
            cbArmor.Checked = s.Armor;
            cbWeapon.Checked = s.Weapon;

            foreach (var r in s.Rates)
                lbGrades.Items.Add(r);

            lblSelectedScroll.Text = s.Name;
            lbGrades.SelectedIndex = 0;
        }

        private void CheckGrade(UpgradeScroll.GradeLevel gl)
        {
            switch (gl)
            {
                case UpgradeScroll.GradeLevel.REVERSE_CONVERSION:
                    rbReverseConversion.Checked = true;
                    break;
                case UpgradeScroll.GradeLevel.REVERSE_STRENGTH:
                    rbReverseStrength.Checked = true;
                    break;
                case UpgradeScroll.GradeLevel.BLESSED:
                    rbBlessed.Checked = true;
                    break;
                case UpgradeScroll.GradeLevel.HIGH:
                    rbHigh.Checked = true;
                    break;
                case UpgradeScroll.GradeLevel.MIDDLE:
                    rbMiddle.Checked = true;
                    break;
                case UpgradeScroll.GradeLevel.LOW:
                    rbLow.Checked = true;
                    break;
            }
        }

        private void CheckType(UpgradeScroll.UpgradeType t)
        {
            switch (t)
            {
                case UpgradeScroll.UpgradeType.REVERSE:
                    rbReverse.Checked = true;
                    break;
                case UpgradeScroll.UpgradeType.REGULAR:
                    rbNormal.Checked = true;
                    break;
                case UpgradeScroll.UpgradeType.ELEMENT:
                    rbElemental.Checked = true;
                    break;
                case UpgradeScroll.UpgradeType.STAT:
                    rbStat.Checked = true;
                    break;
                case UpgradeScroll.UpgradeType.ACCESSORYCOMPOUND:
                    rbAccessoryCompound.Checked = true;
                    break;
                case UpgradeScroll.UpgradeType.ACCESSORYENCHANT:
                    rbAccessoryEnchant.Checked = true;
                    break;
            }
        }


        // update current entry
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int newCost, newPercent, newModifier, newTrinaPercent;
            var selectedIndex = lbGrades.SelectedIndex;
            try
            {
                newCost = int.Parse(txtCost.Text);
                newPercent = int.Parse(txtPercent.Text);
                newModifier = int.Parse(txtModifier.Text);
                newTrinaPercent = (_gradeLevel == UpgradeScroll.GradeLevel.BLESSED || _gradeLevel == UpgradeScroll.GradeLevel.REVERSE_STRENGTH) ? int.Parse(txtTrinaPercent.Text) : 0;
            }
            catch
            {
                MessageBox.Show(@"Cost and Percent must be valid integers (Cost 0 - 2100000000; Percent 0 - 10000)", @"Invalid data");
                return;
            }

            if (newPercent > 10000 || newPercent < 0)
                newPercent = 0;

            if (newCost < 0)
                newCost = 0;

            UpgradeScroll s = _scrolls[((UpgradeScroll)lbScrolls.SelectedItem).ID];
            s.Armor = cbArmor.Checked;
            s.Weapon = cbWeapon.Checked;
            s.Accessory = cbAccessory.Checked;
            if (s.Accessory && s.Rates.Count < 21)
            {
                for (int i = 1; s.Rates.Count < 21; i++)
                {
                    s.Rates.Add(new Rate() { Cost = 0, Grade = i + 10, Percent = 10000 });
                }
            }

            if (!s.Accessory && s.ID != 379257000 && s.Rates.Count > 11)
            {
                while (s.Rates.Count > 11)
                {
                    s.Rates.RemoveAt(s.Rates.Count - 1);
                }
            }
            s.Type = _type;
            s.Grade = _gradeLevel;
            s.Modifier = newModifier;
            s.Rates[lbGrades.SelectedIndex].Cost = newCost;
            s.Rates[lbGrades.SelectedIndex].Percent = newPercent;
            s.Rates[lbGrades.SelectedIndex].TrinaPercent = newTrinaPercent;


            lbScrolls.Items[lbScrolls.SelectedIndex] = s;
            lbGrades.SelectedIndex = selectedIndex;
        }

        // save scrolls and their rates
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveScrolls();
        }

        private void btnDump_Click(object sender, EventArgs e)
        {
            btnDump.Enabled = false;

            CURRENT_WEAPON_INDEX = WEAPON_INDEX;
            CURRENT_ARMOR_INDEX = ARMOR_INDEX;
            CURRENT_ACCESSORY_INDEX = ACCESSORY_INDEX;
            _upgradeRows.Clear();

            /* Generic weapon & accessory & armor upgrades */
            SetGenericUpgrades();

            /* NON - GENERICS */
            SetUniqueWeaponUpgrades();
            SetUniqueAccessorryUpgrades();

            SetReverseUniqueUnlockUpgrades();
            SetReverseUniqueItemUpgrades();
        

            DumpToSQL();
            btnDump.Enabled = true;
        }


        private void rbGrade_CheckedChanged(object sender, EventArgs e)
        {
            _gradeLevel = (UpgradeScroll.GradeLevel)Enum.Parse(typeof(UpgradeScroll.GradeLevel), ((string)((RadioButton)sender).Tag));

            if (_gradeLevel == UpgradeScroll.GradeLevel.BLESSED)
            {
                txtTrinaPercent.Visible = true;
                lblTrinaPercent.Visible = true;
                lblTrinaPercent.Text = "Trina percent";
            }
            else if(_gradeLevel == UpgradeScroll.GradeLevel.REVERSE_STRENGTH)
            {
                txtTrinaPercent.Visible = true;
                lblTrinaPercent.Visible = true;
                lblTrinaPercent.Text = "Karibedis percent";
            }
            else
            {
                txtTrinaPercent.Visible = false;
                lblTrinaPercent.Visible = false;
            }
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            _type = (UpgradeScroll.UpgradeType)Enum.Parse(typeof(UpgradeScroll.UpgradeType), ((string)((RadioButton)sender).Tag));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //SetReverseUniqueUnlockUpgrades();
        }

        #endregion

        #region Rebirth Unique Item Upgrades

        private void SetReverseUniqueUnlockUpgrades()
        {
            var list = new List<string>();

              /* Iterate over item_org_us.tbl */
            foreach (DataRow row in StaticReference.TableSet.Tables["item_org_us.tbl"].Rows)
            {
                #region Generate reverse unique weapon upgrades

                StringBuilder bld = new StringBuilder();
                var normalBaseItemID = Convert.ToInt32(row[0]);
                var normalBaseSlot = Convert.ToInt16(row[12]);
                var isBaseUnique = Convert.ToInt32(row[4]);
                var normalBaseItemName = Convert.ToString(row[2]);

               /* if (isBaseUnique == 0)
                    continue;*/

                /* We don't interested in items other than weapons. */
                if (normalBaseItemID > 920000000 || normalBaseSlot > 4)
                    continue;

                var itemname = Convert.ToString(row[2]);
                var normalBaseExtensionID = Convert.ToByte(row[1]);



                /* Skip reverse and type 22. */ /* armor and accessories*/
                if (normalBaseExtensionID == 22 || normalBaseExtensionID > 23 || (normalBaseExtensionID >= 13 && normalBaseExtensionID <= 21))
                    continue;


                var normalExtensions = StaticReference.GenerateVariationListN(normalBaseExtensionID, normalBaseItemID);

                /* If the variation list is empty, there's no further required action exist. */
                if (!normalExtensions.Any())
                    continue;

          
                foreach (var v in normalExtensions)
                {

                    var baseItem = v[0];
                    var baseItemName = Convert.ToString(baseItem[1]);
                    /*if (baseItemName.ToLowerInvariant().Contains("lupus"))
                        continue;*/
                    var reverseBase = StaticReference.GetReverseBaseVariant(normalBaseItemName);
                    if (reverseBase == null)
                    {
                        continue;
                    }

                    uint reverseBaseItemID = Convert.ToUInt32(reverseBase[0]);
                    string reverseBaseItemName = Convert.ToString(reverseBase[2]);
                    byte reverseBaseExtensionID = (byte)Convert.ToInt32(reverseBase[1]);

                    var reverseExtensions = StaticReference.GenerateVariationListCS(reverseBaseExtensionID, (int)reverseBaseItemID);

                    List<DataRow> currentReverseExtension = reverseExtensions.FirstOrDefault(h => Convert.ToString(h[0][1]).Contains(baseItemName));

                    if (currentReverseExtension == null || currentReverseExtension.Count < 30)
                        continue;


                    var extensionMatchupList = new Dictionary<ItemInfo, ItemInfo>();

                    foreach (var currentNormalExtension in v.Except(new[] {baseItem}))
                    {
                        var currentNormalExtensionID = Convert.ToInt32(currentNormalExtension[0]);
                        var currentNormalExtensionName = Convert.ToString(currentNormalExtension[1]);

                        switch (currentNormalExtensionID % 10)
                        {
                            case 7:
                            {
                                ItemInfo normalinfo = new ItemInfo { ID = currentNormalExtensionID, Name = currentNormalExtensionName };
                                ItemInfo reverseinfo = new ItemInfo
                                {
                                    ID = Convert.ToInt32(currentReverseExtension[0][0]),
                                    Name = Convert.ToString(currentReverseExtension[0][1])
                                };
                                extensionMatchupList.Add(normalinfo, reverseinfo);
                            }
                                break;
                            case 8:
                            {
                                ItemInfo normalinfo = new ItemInfo { ID = currentNormalExtensionID, Name = currentNormalExtensionName };
                                ItemInfo reverseinfo = new ItemInfo
                                {
                                    ID = Convert.ToInt32(currentReverseExtension[4][0]),
                                    Name = Convert.ToString(currentReverseExtension[4][1])
                                };
                                extensionMatchupList.Add(normalinfo, reverseinfo);
                            }
                                break;
                            case 9:
                            {
                                ItemInfo normalinfo = new ItemInfo { ID = currentNormalExtensionID, Name = currentNormalExtensionName };
                                ItemInfo reverseinfo = new ItemInfo
                                {
                                    ID = Convert.ToInt32(currentReverseExtension[10][0]),
                                    Name = Convert.ToString(currentReverseExtension[10][1])
                                };
                                extensionMatchupList.Add(normalinfo, reverseinfo);
                            }
                                break;
                            case 0:
                            {
                                ItemInfo normalinfo = new ItemInfo { ID = currentNormalExtensionID, Name = currentNormalExtensionName };
                                ItemInfo reverseinfo = new ItemInfo
                                {
                                    ID = Convert.ToInt32(currentReverseExtension[20][0]),
                                    Name = Convert.ToString(currentReverseExtension[20][1])
                                };
                                extensionMatchupList.Add(normalinfo, reverseinfo);
                            }
                                break;
                        }
                       
                    }

                    foreach (var q in extensionMatchupList)
                    {
                        bld.Append("Normal base name : " + q.Key.Name + ", normal base extension : " + q.Key.ID +
                                   ", item id : " + (int)(normalBaseItemID + q.Key.ID));
                        bld.AppendLine("");
                        bld.Append("Reverse base name : " + q.Value.Name + ", reverse base extension : " + q.Value.ID +
                                   ", item id : " + (int)(reverseBaseItemID + q.Value.ID) + "\n");
                        bld.AppendLine("");
                        list.Add(bld.ToString());
                        bld = new StringBuilder("");
                    }


                    foreach (var q in extensionMatchupList)
                    {
                        int normalid = normalBaseItemID + q.Key.ID;
                        int reverseid = (int) (reverseBaseItemID + q.Value.ID);
                        Trace.WriteLine("-------------------------------------------");
                        Trace.WriteLine("Normal base name : " + q.Key.Name + ", normal base extension : " + q.Key + ", item id : " + (int) (normalBaseItemID + q.Key.ID));

                        Trace.WriteLine("Reverse base name : " + q.Value.Name + ", reverse base extension : " + q.Value + ", item id : " + (int) (reverseBaseItemID + q.Value.ID));
                        Trace.WriteLine("-------------------------------------------");
                        //continue;
                        int fukkengiveid = reverseid - normalid;
                        var rx = new UpgradeRow
                        {
                            Index = CURRENT_WEAPON_INDEX++,
                            Item = q.Key.ID,
                            Percent = 10000,
                            Modifier = fukkengiveid,
                            Name = "Unique item reverse unlock(" + q.Key.Name.Replace("'","") + ")",
                            Note = q.Value.Name.Replace("'", ""),
                            Cost = 1200000,
                            Extension = normalBaseExtensionID
                        };
                        rx.RequiredItems[0] = 379256000; /* reverse scroll */
                        _upgradeRows.Add(rx.Index, rx);
                    }
                }

               


                #endregion
            }

       

            File.WriteAllLines("uniqueReverseConversionReport.txt", list.ToArray());
        }

        private void SetReverseUniqueItemUpgrades()
        {
            var list = new List<string>();
            /* Iterate over item_org_us.tbl */
            foreach (DataRow row in StaticReference.TableSet.Tables["item_org_us.tbl"].Rows)
            {
            

                var bld = new StringBuilder();
                var itemid = Convert.ToInt32(row[0]);
                var kind = Convert.ToInt16(row[10]);
                var itemname = Convert.ToString(row[2]);
                var slot = Convert.ToInt16(row[12]);
                var extension = Convert.ToByte(row[1]);

                /* Skip non-reverse and type 42.(and non-weapon items too.) */
                if (extension <= 23 || extension == 42 || slot > 4)
                    continue;

                List<List<DataRow>> variationList = StaticReference.GenerateVariationListC(extension, itemid);

                /* If the variation list is empty, there's no further required action exist. */
                if (!variationList.Any())
                    continue;

                /*  int variation = 0;
                  foreach (var x in variationList)
                  {
                      foreach (var z in x)
                      {
                          Trace.WriteLine("variation : "+ variation + "," +Convert.ToString(z[1]));
                      }
                      variation++;
                  }*/

                foreach (var b in variationList)
                {
                    if (b.Count != 30)
                        continue;

                    var startItem = b[0];

                    var startItemExt = Convert.ToInt32(startItem[0]);
                    var startItemName = Convert.ToString(startItem[1]);

                    bld.Append("item_org base : " + itemname + "(" + itemid + ") - ext(" + extension + ") -");
                    bld.Append("starting item : " + startItemName + "(" + startItemExt + ") - ");

                    bld.AppendLine("");

                    foreach (var lll in b)
                    {
                        bld.AppendLine("    - extension " + Convert.ToString(lll[1]) + ", id : " + Convert.ToInt32(lll[0]));

                    }
                    list.Add(bld.ToString());
                    bld = new StringBuilder("");
                    itemname = itemname.Replace("\'", "");
                    foreach (var us in _scrolls.Values)
                    {
                        var sr = us.Rates[0];
                        if (us.Type != UpgradeScroll.UpgradeType.REVERSE || us.Grade != UpgradeScroll.GradeLevel.REVERSE_STRENGTH)
                            continue;
                        
                        for (var c = us.Grade == UpgradeScroll.GradeLevel.REVERSE_STRENGTH ? 0 : 1; c < 2; c++)
                        {
                            int rindex = 1;
                            foreach (var ile in b)
                            {
                                var ileext = Convert.ToInt32(ile[0]);

                                var r = new UpgradeRow
                                {
                                    Index = CURRENT_WEAPON_INDEX++,
                                    Name = "Reverse Unique Upgrade (" + itemname + ")",
                                    Note = "(+" + us.Rates[rindex].Grade + " -> +" + (us.Rates[rindex].Grade + 1) + ")",
                                    Cost = sr.Cost,
                                    Item = ileext,
                                    Modifier = 1,
                                    Extension = extension
                                };
                                if (c == 1)
                                {
                                    r.RequiredItems[0] = KARIVIDIS_ID;
                                    r.RequiredItems[1] = us.ID;
                                    r.Percent = us.Rates[rindex].TrinaPercent;
                                    r.Name += "(with karividis)";
                                }
                                else
                                {
                                    r.RequiredItems[0] = us.ID;
                                    r.Percent = us.Rates[rindex].Percent;
                                }

                                _upgradeRows.Add(r.Index, r);
                                if (++rindex == 30)
                                    break;
                            }
                        }
                    }
                }

            }
            File.WriteAllLines("reverseUniqueUpgradeReport.txt", list.ToArray());
        }
        #endregion

        #region Unique Item Upgrades
        private void SetUniqueAccessorryUpgrades()
        {
             var list = new List<string>();
            /* Iterate over item_org_us.tbl */
            foreach (DataRow row in StaticReference.TableSet.Tables["item_org_us.tbl"].Rows)
            {
                #region Generate unique accessory upgrades
                var bld = new StringBuilder();
                var itemid = Convert.ToInt32(row[0]);
                var kind = Convert.ToInt16(row[10]);
                var itemname = Convert.ToString(row[2]);
                var extension = Convert.ToByte(row[1]);

                /* Skip reverse and type 22. There are no accessories exist below extension 18. */
                if (extension == 22 || extension > 23 || extension < 18)
                    continue;

                /* We don't interested in items other than accessorries. */
                switch (kind)
                {
                    case 91: // earring
                    case 92: // necklace
                    case 93: // ring
                    case 94: // belt
                        break;
                    /* Skip others */
                    default:
                        continue;
                }

                var variationList = StaticReference.GenerateVariationListN(extension, itemid);

                /* If the variation list is empty, there's no further required action exist. */
                if (!variationList.Any())
                    continue;

                foreach (var v in variationList.Where(v => v.Count >= 11))
                {
                    /* Remove unrequired (+10) entry */
                  //  v.RemoveAt(2);
                    /* Remove variations which grade is higher than 5 */
                    v.RemoveRange(6, 5);

                    DataRow baseItem = v[0];
                    DataRow unlockItem = v[1];
                    int baseItemExt = Convert.ToInt32(baseItem[0]);
                    string baseItemName = Convert.ToString(baseItem[1]);
                    int unlockItemExt = Convert.ToInt32(unlockItem[0]);
                    string unlockItemName = Convert.ToString(unlockItem[1]);


                    bld.Append("item_org base : " + itemname + "(" + itemid + ") - ");
                    bld.Append("matched unique base : " + baseItemName + "(" + baseItemExt + ") - ");
                    bld.Append("unlock item : " +unlockItemName+"(" + unlockItemExt+")");
                    bld.AppendLine("");

                    foreach (var lll in v.Except(new[] { baseItem }))
                    {
                        bld.AppendLine("    - extension " + Convert.ToString(lll[1]) + ", id : " + Convert.ToInt32(lll[0]));
                    }
                    list.Add(bld.ToString());
                    bld = new StringBuilder("");
                    itemname = itemname.Replace("\'", "");

                    foreach (var us in _scrolls.Values)
                    {
                        var sr = us.Rates[0];

                        if (us.Type == UpgradeScroll.UpgradeType.ACCESSORYCOMPOUND)
                        {

                            var first = new UpgradeRow
                            {
                                Index = CURRENT_ACCESSORY_INDEX++,
                                Name = "Unique Accessory First Upgrade (" + itemname + ")",
                                Note = "(+" + sr.Grade + " -> +" + (sr.Grade + 1) + ")",
                                Cost = sr.Cost,
                                /* Origin ID */
                                Item = baseItemExt,
                                /* nGiveItem*/
                                Modifier = baseItemExt < unlockItemExt ? (unlockItemExt - baseItemExt) : (-1)*(baseItemExt - unlockItemExt),
                                Percent = sr.Percent,
                                Extension = extension
                            };
                            first.RequiredItems[0] = us.ID;

                            _upgradeRows.Add(first.Index, first);


                            /* Other rows */

                            int rindex = 1;
                            foreach (var ile in v.Except(new[] { baseItem }))
                            {
                                var ileext = Convert.ToInt32(ile[0]);
                                var r = new UpgradeRow
                                {
                                    Index = CURRENT_ACCESSORY_INDEX++,
                                    Name = "Unique Accessory Upgrade (" + itemname + ")",
                                    Note = "(+" + us.Rates[rindex].Grade + " -> +" + (us.Rates[rindex].Grade + 1) + ")",
                                    Cost = sr.Cost,
                                    Item = ileext,
                                    Modifier = 1,
                                    Percent = us.Rates[rindex].Percent,
                                    Extension = extension
                                };
                                r.RequiredItems[0] = us.ID;
                                _upgradeRows.Add(r.Index, r);
                                if (++rindex == 5)
                                    break;
                            }
                        }
                      

                    }
                }

                #endregion
            }

            File.WriteAllLines("accessoryReport.txt", list.ToArray());
              
        }

        private void SetUniqueWeaponUpgrades()
        {
            var list = new List<string>();
            /* Iterate over item_org_us.tbl */
            foreach (DataRow row in StaticReference.TableSet.Tables["item_org_us.tbl"].Rows)
            {
                #region Generate unique weapon upgrades
                StringBuilder bld = new StringBuilder();
                var itemid = Convert.ToInt32(row[0]);
                var slot = Convert.ToInt16(row[12]);

                /* We don't interested in items other than weapons. */
                if (itemid > 920000000 || slot > 4)
                    continue;

                var itemname = Convert.ToString(row[2]);
                var extension = Convert.ToByte(row[1]);

              

                /* Skip reverse and type 22. */          /* armor and accessories*/
                if (extension == 22 || extension > 23 || (extension >= 13 && extension <= 21))
                    continue;


                var variationList = StaticReference.GenerateVariationListN(extension, itemid);

                if(itemid == 180110000)
                {
                    Debug.Print("test");
                }

                /* If the variation list is empty, there's no further required action exist. */
                if (!variationList.Any())
                    continue;

                
                foreach (var v in variationList.Where(v => v.Count >= 11))
                {
                    /* Remove unrequired (+10) entry */
                  //  v.RemoveAt(2);

                    DataRow baseItem = v[0];
                    DataRow unlockItem = v[1];
                    int baseItemExt = Convert.ToInt32(baseItem[0]);
                    string baseItemName = Convert.ToString(baseItem[1]);
                    int unlockItemExt = Convert.ToInt32(unlockItem[0]);
                    string unlockItemName = Convert.ToString(unlockItem[1]).Replace('\'', '`');


                    bld.Append("item_org base : " + itemname + "(" + itemid + ") - ");
                    bld.Append("matched unique base : " + baseItemName + "(" + baseItemExt + ") - ");
                    bld.Append("unlock item : " +unlockItemName+"(" + unlockItemExt+")");
                    bld.AppendLine("");
                    
                    foreach (var lll in v.Except(new[] { baseItem }))
                    {
                        bld.AppendLine("    - extension " + Convert.ToString(lll[1]) + ", id : " + Convert.ToInt32(lll[0]));

                    }
                    list.Add(bld.ToString());
                    bld = new StringBuilder("");
                    itemname = itemname.Replace("\'", "");

                    foreach (var us in _scrolls.Values)
                    {
                        var sr = us.Rates[0];

                        if (us.Grade != UpgradeScroll.GradeLevel.BLESSED)
                            continue;

                        if (us.Type == UpgradeScroll.UpgradeType.ELEMENT)
                        {
                            var r = new UpgradeRow
                            {
                                Index = CURRENT_WEAPON_INDEX++,
                                Name = "Unique Weapon Elemental Unlock (" + unlockItemName + ")",
                                Note = "(+" + sr.Grade + " -> +" + (sr.Grade + 1) + ")",
                                Cost = sr.Cost,
                                /* Origin ID */
                                Item = baseItemExt,
                                /* nGiveItem*/
                                Modifier = baseItemExt < unlockItemExt ? (unlockItemExt - baseItemExt) : (-1)*(baseItemExt - unlockItemExt),
                                Percent = sr.Percent,
                                Extension = extension
                            };
                            r.RequiredItems[0] = us.ID;

                            _upgradeRows.Add(r.Index, r);
                        }
                        else if (us.Type == UpgradeScroll.UpgradeType.REGULAR)
                        {
                            for (var c = us.Grade == UpgradeScroll.GradeLevel.BLESSED? 0 : 1; c < 2; c++)
                            {
                                int rindex = 1;
                                foreach (var ile in v.Except(new[] { baseItem }))
                                {
                                    var ileext = Convert.ToInt32(ile[0]);
                                    var r = new UpgradeRow
                                    {
                                        Index = CURRENT_WEAPON_INDEX++,
                                        Name = "Unique Weapon Upgrade (" + unlockItemName + ")",
                                        Note = "(+" + us.Rates[rindex].Grade + " -> +" + (us.Rates[rindex].Grade + 1) + ")",
                                        Cost = sr.Cost,
                                        Item = ileext,
                                        Modifier = 1,

                                        Extension = extension
                                    };
                                    if (c == 0)
                                    {
                                        r.RequiredItems[0] = TRINA_ID;
                                        r.RequiredItems[1] = us.ID;
                                        r.Name += "(with trina)";
                                        r.Percent = us.Rates[rindex].TrinaPercent;
                                    }
                                    else
                                    {
                                        r.RequiredItems[0] = us.ID;
                                        r.Percent = us.Rates[rindex].Percent;
                                    }
                                    _upgradeRows.Add(r.Index, r);
                                    if (++rindex == 10)
                                        break;
                                }
                            }
                            
                        }

                    }
                }

                #endregion
            }

            File.WriteAllLines("uniqueWeaponUpgradeReport.txt", list.ToArray());
          
        }

      //  private void SetUnique
        #endregion

        #region Generic Item Upgrades

        private void SetGenericUpgrades()
        {
            

            foreach (var s in _scrolls.Values)
            {

                switch (s.Type)
                {

                    case UpgradeScroll.UpgradeType.REGULAR:
                    case UpgradeScroll.UpgradeType.ACCESSORYCOMPOUND:
                        for (var i = 1; i < 10; i++)
                            SetGenericItemUpgrades(s, i);
                        break;
                    case UpgradeScroll.UpgradeType.ACCESSORYENCHANT:
                            SetGenericAccessoryExtraStatUnlocks(s);
                        break;
                    case UpgradeScroll.UpgradeType.ELEMENT:
                        for (var i = 1; i < 10; i++)
                            SetGenericElementalUpgrade(s, i);
                        break;
                    case UpgradeScroll.UpgradeType.REVERSE:
                    {
                        if (s.Grade == UpgradeScroll.GradeLevel.REVERSE_CONVERSION)
                            SetGenericItemReverseUnlock(s);
                        else
                            for (var i = 1; i < 30; i++)
                                SetGenericReverseUpgrades(s, i);
                    }
                        break;

                    default:
                        {

                            for (var i = 1; i < 11; i++)
                            {

                                var sr = s.Rates[i];
                                var r = new UpgradeRow();
                                if (s.Weapon)
                                    r = new UpgradeRow(CURRENT_WEAPON_INDEX++, s.Modifier, "Generic Weapon Alteration",
                                        sr.Cost);
                                else if (s.Armor)
                                    r = new UpgradeRow(CURRENT_ARMOR_INDEX++, s.Modifier, "Generic Armor Alteration",
                                        sr.Cost);

                                r.RequiredItems[0] = s.ID;
                                r.Item = i;
                                r.Percent = sr.Percent;
                                _upgradeRows.Add(r.Index, r);
                            }
                        }
                        break;
                }
            }
        }

        private void SetGenericReverseUpgrades(UpgradeScroll s, int i)
        {

            /* Generic Weapon Upgrade */
            if (s.Weapon)
            {
                for (var c = s.Grade == UpgradeScroll.GradeLevel.REVERSE_STRENGTH ? 0 : 1; c < 2; c++)
                {
                    string[] weaponUpgradeNames = { "Reduce", "Weapon-specific Attribute 1", "Weapon-specific Attribute 2", "Weapon-specific Attribute 3" };
                    int[] weaponUpgradeIDs = { 0, 30, 60, 90 };
                    for (var n = 0; n < weaponUpgradeNames.Length; n++)
                    {
                        var val = weaponUpgradeNames[n];
                        var sr = s.Rates[i];
                        var r = new UpgradeRow(CURRENT_WEAPON_INDEX++, s.Modifier,
                            "Generic Reverse Weapon Upgrade (" + val + ")", sr.Cost,
                            "(+" + sr.Grade + " -> +" + (sr.Grade + 1) + ")");
                        if(c == 1)
                        {
                            r.RequiredItems[0] = KARIVIDIS_ID;
                            r.RequiredItems[1] = s.ID;
                            r.Percent = sr.TrinaPercent;
                            r.Name += "(with karividis)";
                        }
                        else
                        {
                            r.RequiredItems[0] = s.ID;
                            r.Percent = sr.Percent;
                        }
                      
                        r.Item = weaponUpgradeIDs[n] + sr.Grade;
                        
                        _upgradeRows.Add(r.Index, r);
                    }
                }
              
            }
            /* Generic Armor Upgrade */
            if (s.Armor)
            {
                for (var c = s.Grade == UpgradeScroll.GradeLevel.REVERSE_STRENGTH ? 0 : 1; c < 2; c++)
                {

                    string[] armorUpgradeNames =
                {
                    "Strength", "Health", "Dexterity", "Intelligence", "Magic Power",
                    "Resistance", "Reduced", "Misc. AC"
                };
                    int[] armorUpgradeIDs = { 0, 30, 60, 90, 120, 150, 180, 210 };
                    for (var n = 0; n < armorUpgradeNames.Length; n++)
                    {
                        var val = armorUpgradeNames[n];
                        var sr = s.Rates[i];
                        var r = new UpgradeRow(CURRENT_ARMOR_INDEX++, s.Modifier,
                            "Generic Reverse Armor Upgrade (" + val + ")", sr.Cost,
                            "(+" + sr.Grade + " -> +" + (sr.Grade + 1) + ")");

                        if (c == 1)
                        {
                            r.RequiredItems[0] = KARIVIDIS_ID;
                            r.RequiredItems[1] = s.ID;
                            r.Percent = sr.TrinaPercent;
                            r.Name += "(with karividis)";
                        }
                        else
                        {
                            r.RequiredItems[0] = s.ID;
                            r.Percent = sr.Percent;
                        }

                        r.Item = armorUpgradeIDs[n] + sr.Grade;
                       
                        _upgradeRows.Add(r.Index, r);
                    }
                }
            }
        }
        private void SetGenericItemReverseUnlock(UpgradeScroll s)
        {


            if (s.Weapon)
            {
                #region Weapon unlock
                var extensionIDs = new List<int> {37, 38, 39, 40, 47, 48, 49, 50};

                foreach (var extID in extensionIDs)
                {
                    /* + 1000 comes from base conversion */
                    /* item id - extension gives us the base id */
                    /* so, we should subtract that extension id from give item value. */

                    var giveItemValue = 1000;
                    string note = "";
                    string name = "Generic weapon reverse unlock";
                    /* new ext = ext + 24 */
                    switch (extID)
                    {
                        /* Reduce*/
                        case 37:
                            giveItemValue = 964;
                            note = "(+7) -> (+1)";
                            name += " (reduced)";
                            break;
                        case 38:
                            giveItemValue = 967;
                            note = "(+8) -> (+5)";
                            name += " (reduced)";
                            break;
                        case 39:
                            giveItemValue = 972;
                            note = "(+9) -> (+11)";
                            name += " (reduced)";
                            break;
                        case 40:
                            giveItemValue = 981;
                            note = "(+10) -> (+21)";
                            name += " (reduced)";
                            break;
                        /* Attribute */
                        case 47:
                            giveItemValue = 984;
                            note = "(+7) -> (+1)";
                            name += " (main attribute)";
                            break;
                        case 48:
                            giveItemValue = 987;
                            note = "(+8) -> (+5)";
                            name += " (main attribute)";
                            break;
                        case 49:
                            giveItemValue = 992;
                            note = "(+9) -> (+11)";
                            name += " (main attribute)";
                            break;
                        case 50:
                            giveItemValue = 1001;
                            note = "(+10) -> (+21)";
                            name += " (main attribute)";
                            break;
                    }

                    var rx = new UpgradeRow
                    {
                        Index = CURRENT_WEAPON_INDEX++,
                        Item = extID,
                        Percent = s.Rates[0].Percent,
                        Modifier = giveItemValue,
                        Name = name,
                        Note = note,
                        Cost = s.Rates[0].Cost,
                        Extension = -1
                    };
                    rx.RequiredItems[0] = s.ID; /* reverse upgrade scroll */
                    _upgradeRows.Add(rx.Index, rx);

                }
                #endregion
            }
            if (s.Armor)
            {
                #region Armor unlock
                /* base difference + 10000000 */
                var extensionIDs = new Dictionary<int,int>
                {
                    {17,1},{18,5},{19,11},{20,21}, /* str */
                    {27,151},{28,155},{29,161},{30,171},  /* resist */
                    {37,181},{38,185},{39,191},{40,201},  /* reduce*/
                   /* 47,48,49,50,*/ /* special ac mixed */
                    {507,31},{508,35},{509,41},{510,51}, /* hp */
                    {517,61},{518,65},{519,71},{520,81}, /* dex */
                    {527,91},{528,95},{529,101},{530,111}, /* int */
                    {537,121},{538,125},{539,131},{540,141}, /* mp */
                    {547,211},{548,215},{549,221},{550,231} /* special ac solo */
                };

                /* 1 -30 str*/
                /* 31 - 60 hp*/
                /* 61-90 dex*/
                /* 91 120 int*/
                /* 121 150 mp*/
                /* 151 180 resist */
                /* 181 210 reduce */
                /* 211 240 special ac solo */
                int itr = 0;
                foreach (var extID in extensionIDs)
                {
                    var giveItemValue = (extID.Value - extID.Key) + 10000000;
                    int ngrade = 0;
                    int rgrade = 0;
                    switch (itr++)
                    {
                        case 0:
                            ngrade = 7;
                            rgrade = 1;
                            break;
                        case 1:
                            ngrade = 8;
                            rgrade = 5;
                            break;
                        case 2:
                            ngrade = 9;
                            rgrade = 11;
                            break;
                        case 3:
                            ngrade = 10;
                            rgrade = 21;
                            itr = 0;
                            break;
                    }
                    string note = string.Format("(+{0}) -> (+{1})", ngrade, rgrade);
                    const string name = "Generic armor reverse unlock";

                    var rx = new UpgradeRow
                    {
                        Index = CURRENT_ARMOR_INDEX++,
                        Item = extID.Key,
                        Percent = s.Rates[0].Percent,
                        Modifier = giveItemValue,
                        Name = name,
                        Note = note,
                        Cost = s.Rates[0].Cost,
                        Extension = -1
                    };
                    rx.RequiredItems[0] = s.ID; /* reverse upgrade scroll */
                    _upgradeRows.Add(rx.Index, rx);
                  
                }

                #endregion
            }

        }
        private void SetGenericAccessoryExtraStatUnlocks(UpgradeScroll s)
        {
            var sr = s.Rates[0];
            var items = new Dictionary<int, int>();
            switch (s.ID)
            {
                case 379160000: // str
                    items.Add(20, 531); // hp & str
                    items.Add(30, 571); // dex & str
                    items.Add(40, 611); // int & str
                    items.Add(80, 621); // mp & str
                    break;
                case 379161000: // hp
                    items.Add(10, 491); // str & hp
                    items.Add(30, 581); // dex & hp
                    items.Add(40, 621); // int & hp
                    items.Add(80, 631); // mp & hp
                    break;
                case 379162000: //dex
                    items.Add(10, 501); // str & dex
                    items.Add(20, 541); // hp & dex
                    items.Add(40, 631); // int & dex
                    items.Add(80, 641); // mp & dex
                    break;
                case 379163000: // int
                    items.Add(10, 511); // str & int
                    items.Add(20, 551); // hp & int
                    items.Add(30, 591); // dex & int
                    items.Add(80, 651); // mp & int
                    break;
                case 379164000: // mp
                    items.Add(10, 521); // str & mp
                    items.Add(20, 561); // hp & mp
                    items.Add(30, 601); // dex & mp
                    items.Add(40, 641); // int & mp
                    break;
            }
            foreach (var kvp in items)
            {
                var rx = new UpgradeRow
                {
                    Index = CURRENT_ACCESSORY_INDEX++,
                    Item = kvp.Key,
                    Percent = sr.Percent,
                    Modifier = kvp.Value,
                    Name = "Generic Accessory Extra Stat Unlock",
                    Note = "(+10) -> (+11)",
                    Cost = sr.Cost,
                    Extension = -1,
                };
                rx.RequiredItems[0] = s.ID;
                _upgradeRows.Add(rx.Index, rx);
            }
        }
        private void SetGenericItemUpgrades(UpgradeScroll s, int i)
        {
            for (var c = s.Grade == UpgradeScroll.GradeLevel.BLESSED ? 0 : 1; c < 2; c++)
            {
                var sr = s.Rates[i];
                UpgradeRow r;
                switch (s.Type)
                {
                    case UpgradeScroll.UpgradeType.REGULAR:
                        #region Generic Item Upgrades
                        /*
                         *  The extensions 10,20,30,40 are the primary attributes defined by the extension
                         *  The 5xx extensions are other primary attributes besides them (used for shields and staffs mostly, and also armors)
                         */
                        string[] weaponUpgradeNames = { "", "Primary Stat", "Primary Resistance", "Reduced", "Primary Attribute Default", "Primary Attribute 1", "Primary Attribute 2", "Primary Attribute 3", "Primary Attribute 4", "Primary Attribute 5"};
                        int[] weaponUpgradeIDs = { 0, 10, 20, 30, 40, 500, 510, 520, 530, 540 };

                        for (int n = 0; n < weaponUpgradeNames.Length; n++)
                        {
                            r = new UpgradeRow(CURRENT_WEAPON_INDEX++, s.Modifier, "Generic Weapon Upgrade" + (weaponUpgradeNames[n].Length > 0 ? " (" + weaponUpgradeNames[n] + ")" : ""), sr.Cost, "(+" + sr.Grade + " -> +" + (sr.Grade + 1) + ")");
                            if (c == 0)
                            {
                                r.RequiredItems[0] = TRINA_ID;
                                r.RequiredItems[1] = s.ID;
                                r.Name += "(with trina)";
                            }
                            else
                                r.RequiredItems[0] = s.ID;

                            r.Item = weaponUpgradeIDs[n] + sr.Grade;
                            r.Percent = c == 0 ? sr.TrinaPercent : sr.Percent;
                            _upgradeRows.Add(r.Index, r);

                        }

                        string[] armorUpgradeNames =
                        {
                            "No bonus", "Strength", "Resistance", "Reduce",
                            "Special AC (Mixed)", "Health", "Dexterity", "Intelligence", "Magic Power",
                            "Special AC (Solo)", /*"Health and Strength", "Health and Dexterity",
                            "Health and Intelligence", "Health and Magic Power"*/
                        };
                        int[] armorUpgradeIDs = {0, 10, 20, 30, 40, 500, 510, 520, 530, 540/*, 550, 560, 570, 580*/};

                        for (int n = 0; n < armorUpgradeNames.Length; n++)
                        {
                            r = new UpgradeRow(CURRENT_ARMOR_INDEX++, s.Modifier, "Generic Armor Upgrade" + (armorUpgradeNames[n].Length > 0 ? " (" + armorUpgradeNames[n] + ")" : ""), sr.Cost, "(+" + sr.Grade + " -> +" + (sr.Grade + 1) + ")");
                            if (c == 0)
                            {
                                r.RequiredItems[0] = TRINA_ID;
                                r.RequiredItems[1] = s.ID;
                                r.Name += "(with trina)";
                            }
                            else
                                r.RequiredItems[0] = s.ID;
                            r.Item = i + armorUpgradeIDs[n];
                            r.Percent = c == 0 ? sr.TrinaPercent : sr.Percent;
                            _upgradeRows.Add(r.Index, r);
                            /* No need for high-armor impl.*/
                          /*  r = r.Clone();
                            r.Index = CURRENT_HIGHARMOR_INDEX++;
                            _upgradeRows.Add(r.Index, r);*/
                        }
                        #endregion
                        break;
                    case UpgradeScroll.UpgradeType.ACCESSORYCOMPOUND:
                        #region Generic Accessory Upgrades
                        int[] statBases = { 0, 10, 20, 30, 70 };
                        int[] extraStatBases = { 500, 510, 520, 530, 550, 560, 570, 580, 600, 610, 620, 630, 650, 660, 670, 680, 700, 710, 720, 730 };

                        foreach (var statBase in statBases)
                        {
                            r = new UpgradeRow
                            {
                                Index = CURRENT_ACCESSORY_INDEX++,
                                Modifier = s.Modifier,
                                Name = "Generic Accessory Upgrade",
                                Cost = sr.Cost,
                                Note = string.Format("(+{0}) -> (+{1})", sr.Grade, sr.Grade + 1),
                                Item = i + statBase,
                                Percent = sr.Percent,
                            };
                            r.RequiredItems[0] = s.ID;
                            _upgradeRows.Add(r.Index, r);
                        }

                        /* Starting from 11 to 20 */
                        sr = s.Rates[i + 10];

                        foreach (var extraStatBase in extraStatBases)
                        {
                            r = new UpgradeRow
                            {
                                Index = CURRENT_ACCESSORY_INDEX++,
                                Modifier = s.Modifier,
                                Name = "Generic Accessory Upgrade",
                                Cost = sr.Cost,
                                Note = string.Format("(+{0}) -> (+{1})", sr.Grade, sr.Grade + 1),
                                Item = i + extraStatBase,
                                Percent = sr.Percent,
                            };
                            r.RequiredItems[0] = s.ID;
                            _upgradeRows.Add(r.Index, r);
                        }
                        #endregion 
                        break;
                }
            }
        }
        private void SetGenericElementalUpgrade(UpgradeScroll s, int i)
        {

            var r = new UpgradeRow
            {
                Index = CURRENT_WEAPON_INDEX++,
                Extension = -1,
                Cost = s.Rates[i].Cost,
                Percent = s.Rates[i].Percent
            };
            r.RequiredItems[0] = s.ID;
            r.Item = i;
            r.Modifier = s.Modifier;
            _upgradeRows.Add(r.Index, r);
            r.Percent = s.Rates[i].Percent;
            r.Name = "Generic Weapon Elemental Unlock";
            r.Note = s.Name;
        }
        #endregion 

        #region Scroll List Load / Save / Set

        private void SaveScrolls()
        {
            Stream stream = File.Open("scrolls.ob", FileMode.Create);
            BinaryFormatter bformatter = new BinaryFormatter();
            bformatter.Serialize(stream, _scrolls);
            stream.Close();
        }


        private void LoadScrolls()
        {
            try
            {
                Stream stream = File.Open("scrolls.ob", FileMode.Open);
                BinaryFormatter bformatter = new BinaryFormatter();
                _scrolls = (Dictionary<int, UpgradeScroll>)bformatter.Deserialize(stream);
                stream.Close();
                SetScrollsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"There was a problem reading scrolls.ob - " + ex.Message + ex.StackTrace,@"Upgrade Editor");
            }
        }

        private void SetScrollsList()
        {
            lbScrolls.Items.Clear();
            lbGrades.Items.Clear();

            foreach (UpgradeScroll s in _scrolls.Values)
                lbScrolls.Items.Add(s);

            if (lbScrolls.Items.Count > 0)
                lbScrolls.SelectedIndex = 0;
        }

        #endregion


        private void DumpToSQL()
        {
            using (var file = new StreamWriter("upgrade.sql"))
            {
                file.WriteLine("-- > This file is automatically generated by KOUpgradeEditor <");
                file.WriteLine("-- > Author : PENTAGRAM, original tool made by : Johnny and twostars <");
                file.WriteLine("-- > File generated at : " + DateTime.Now.ToString(CultureInfo.InvariantCulture) + " <");
                file.WriteLine("-- > Row count : " + _upgradeRows.Values.Count + " <");
                file.WriteLine("");
                file.WriteLine("-- IT IS HIGHLY RECOMMENDED TO TAKE BACKUP OF YOUR ITEM_UPGRADE TABLE BEFORE EXECUTING THIS SCRIPT!");
                file.WriteLine("-- CONSIDER YOURSELF WARNED.");
                file.WriteLine("");
                file.WriteLine("TRUNCATE TABLE ITEM_UPGRADE");
                foreach (var r in _upgradeRows.Values)
                {
                    file.WriteLine(r.toInsert());
                }
            }
            MessageBox.Show("Dump complete.");
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }


    }
}
