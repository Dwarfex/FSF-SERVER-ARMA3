﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FSF_Server_A3.Classes;
using System.IO;
using System.Threading;
using System.Globalization;
using Infralution.Localization;

namespace FSF_Server_A3
{
    public partial class FenetrePrincipale : Form
    {
        public FenetrePrincipale()
        {
            Language.DetermineLanguage();                    
            InitializeComponent();
        }

        private void FenetrePrincipale_Load(object sender, EventArgs e)
        {

            label77.TextChanged += label77_TextChanged;
            labelSynchroEnCoursInvisible.EnabledChanged += labelSynchroEnCoursInvisible_EnabledChanged;
            Var.fenetrePrincipale = this;
            Core.InitialiseCore();
        }

        void labelSynchroEnCoursInvisible_EnabledChanged(object sender, EventArgs e)
        {
               if (labelSynchroEnCoursInvisible.Enabled == true)
               {
                   Interface.dessineInterface((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString());            
               }
        }

        void label77_TextChanged(object sender, EventArgs e)
        {
            if (label77.Text.Replace(",", ".") == "0.000 Mo")
            {
                Var.fenetrePrincipale.label77.Text = "a jour";
                Var.fenetrePrincipale.label77.ForeColor = System.Drawing.Color.Black;
                Var.fenetrePrincipale.pictureBox6.Visible = true;
                Var.fenetrePrincipale.pictureBox6.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
            }
            else
            {
                Var.fenetrePrincipale.label77.ForeColor = System.Drawing.Color.Red;
                Var.fenetrePrincipale.pictureBox6.Visible = true;
                Var.fenetrePrincipale.pictureBox6.Image = FSF_Server_A3.Properties.Resources.balle_fermer_cute_stop_icone_4372_64;
            };
        }


        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString() != "")
            {
                Interface.dessineInterface((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {           
            Var.timer.Tick += new EventHandler(TimerEvent);
            Var.timer.Interval = 5000;
            Var.timer.Start();     
            GestionProfil.SauvegardeProfil((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            label4.Visible = true;  
        }

        private static void TimerEvent(Object myObject, EventArgs myEventArgs)
        {
            Var.timer.Stop();
            Var.fenetrePrincipale.label4.Visible = false;
            Var.fenetrePrincipale.label5.Visible = false;
        }

        private void Priorité_Enter(object sender, EventArgs e)
        {
            TabPriority.actualisePrioriteMods();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            TabPriority.topPrioriteMod();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            TabPriority.augmentePrioriteMod();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            TabPriority.diminuePrioriteMod();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            TabPriority.downPrioriteMod();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox7);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox7);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox2);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox3);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox3);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox6);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox6);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox4);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox4);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox5);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox5);
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                comboBox3.Enabled = true;
                if (comboBox3.SelectedIndex == -1)
                { comboBox3.SelectedIndex = 0; };
            }
            else
            {
                comboBox3.Enabled = false;
                comboBox3.SelectedIndex = -1;
                pictureBox26.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                label34.Enabled = false;
                pictureBox28.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                label35.Enabled = false;
                pictureBox30.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                label36.Enabled = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.Text)
            {
                case "0":
                    pictureBox26.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label34.Enabled = false;
                    pictureBox28.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label35.Enabled = false;
                    pictureBox30.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label36.Enabled = false;
                    break;

                case "1":
                    pictureBox26.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label34.Enabled = true;
                    pictureBox28.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label35.Enabled = false;
                    pictureBox30.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label36.Enabled = false;
                    break;
                case "3":
                    pictureBox26.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label34.Enabled = true;
                    pictureBox28.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label35.Enabled = true;
                    pictureBox30.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label36.Enabled = false;
                    break;
                case "5":
                    pictureBox26.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label34.Enabled = true;
                    pictureBox28.Image = FSF_Server_A3.Properties.Resources.fermer_gtk_icone_6139_128;
                    label35.Enabled = false;
                    pictureBox30.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label36.Enabled = true;
                    break;
                case "7":
                    pictureBox26.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label34.Enabled = true;
                    pictureBox28.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label35.Enabled = true;
                    pictureBox30.Image = FSF_Server_A3.Properties.Resources.crochet_ok_oui_icone_5594_64;
                    label36.Enabled = true;
                    break;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked) { trackBar2.Enabled = true; textBox6.Enabled = true; textBox6.Text = trackBar2.Value.ToString(); } else { trackBar2.Enabled = false; textBox6.Enabled = false; }

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) { trackBar1.Enabled = true; textBox5.Enabled = true; textBox5.Text = trackBar1.Value.ToString(); } else { trackBar1.Enabled = false; textBox5.Enabled = false; }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked) { trackBar3.Enabled = true; textBox20.Enabled = true; textBox20.Text = trackBar3.Value.ToString(); } else { trackBar3.Enabled = false; textBox20.Enabled = false; }

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            textBox6.Text = trackBar2.Value.ToString();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox5.Text = trackBar1.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            textBox20.Text = trackBar3.Value.ToString();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_recruit.Visible = radioButton7.Checked;
            groupBox_regular.Visible = radioButton8.Checked;
            groupBox_veteran.Visible = radioButton9.Checked;
            groupBox_mercenary.Visible = radioButton10.Checked;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_recruit.Visible = radioButton7.Checked;
            groupBox_regular.Visible = radioButton8.Checked;
            groupBox_veteran.Visible = radioButton9.Checked;
            groupBox_mercenary.Visible = radioButton10.Checked;
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_recruit.Visible = radioButton7.Checked;
            groupBox_regular.Visible = radioButton8.Checked;
            groupBox_veteran.Visible = radioButton9.Checked;
            groupBox_mercenary.Visible = radioButton10.Checked;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            groupBox_recruit.Visible = radioButton7.Checked;
            groupBox_regular.Visible = radioButton8.Checked;
            groupBox_veteran.Visible = radioButton9.Checked;
            groupBox_mercenary.Visible = radioButton10.Checked;
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {

            numericUpDown3.Enabled = checkBox16.Checked;
            numericUpDown4.Enabled = checkBox16.Checked;
   
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown2.Enabled = checkBox13.Checked;
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {
            numericUpDown10.Enabled = (textBox21.Text != "");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Core.LanceServeur((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
        }

        private void textBox18_Validated(object sender, EventArgs e)
        {
            GestionProfil.SauvegardeProfil((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
            Interface.dessineInterface((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
        }


        private void button37_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog CheminA3Server = new FolderBrowserDialog();
            CheminA3Server.ShowDialog();
            textBox18.Text = CheminA3Server.SelectedPath;
        }

        private void textBox18_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GestionProfil.SauvegardeProfil((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
                Interface.dessineInterface((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
                
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           string nomProfil = Core.ConversionNomFichierValide(textBox1.Text, '_');
            nomProfil = nomProfil.TrimStart();
            string[] listeProfil = Directory.GetFiles(Var.RepertoireDeSauvegarde, "*.profil.xml", SearchOption.TopDirectoryOnly);
            Boolean nomProfilValid = true;
            foreach (var ligne in listeProfil)
            {
                string textCombo = ligne.Replace(Var.RepertoireDeSauvegarde, "");
                string nomProfilATester = textCombo.Replace(".profil.xml", "");
                if (nomProfil == nomProfilATester) { nomProfilValid = false; }
            }
            if (nomProfil == "") { nomProfilValid = false; }

            if (nomProfilValid)
            {
                GestionProfil.CreationNouveauProfil(nomProfil);
                int compteur = 0;
                foreach (ComboboxItem profil in Var.fenetrePrincipale.comboBox4.Items)
                {
                    if (profil.Text.ToString() == nomProfil) { Var.fenetrePrincipale.comboBox4.SelectedIndex = compteur; };
                    compteur++;
                }
            }
            else
            {
                var infoBox = MessageBox.Show("Votre nom de profil n'est pas valide.", "Erreur création profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || (Var.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString() == "" || (Var.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Text.ToString() == (Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString())
            {
                var infoBox = MessageBox.Show("Impossible d'effacer ce profil si il est celui actif.", "Erreur Suppression profil", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    File.Delete(Var.RepertoireDeSauvegarde + listBox1.SelectedItem.ToString() + ".profil.xml");
                    File.Delete(Var.RepertoireDeSauvegarde + listBox1.SelectedItem.ToString() + ".profilPriorite.xml");
                    File.Delete(Var.RepertoireDeSauvegarde + listBox1.SelectedItem.ToString() + ".FSFServer.bin");
                }
                catch
                {
                }
                string profilactif = (Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Text.ToString();
                GestionProfil.InitialiseListeProfil();
                int compteur = 0;
                foreach (ComboboxItem profil in Var.fenetrePrincipale.comboBox4.Items)
                {
                    if (profil.Text.ToString() == profilactif) { Var.fenetrePrincipale.comboBox4.SelectedIndex = compteur; };
                    compteur++;
                }
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedItem != null)
            {
                Var.timer.Tick += new EventHandler(TimerEvent);
                Var.timer.Interval = 5000;
                Var.fenetrePrincipale.label5.Visible = true;
                Var.timer.Start(); 
                string profilChoisis = (Var.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString();
                Core.SetKeyValue(@"Software\Clan FSF\FSF Server A3\", "profil_favoris", (Var.fenetrePrincipale.listBox1.SelectedItem as ComboboxItem).Value.ToString());
            }
        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            TabPriority.topPrioriteMod();
        }

        private void button29_Click_1(object sender, EventArgs e)
        {
            TabPriority.augmentePrioriteMod();
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            TabPriority.diminuePrioriteMod();
        }

        private void button32_Click_1(object sender, EventArgs e)
        {
            TabPriority.downPrioriteMod();
        }


        private void button25_Click(object sender, EventArgs e)
        {              
 
                Core.synchroRsync();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
           TabMissions.changeFiltreMissions((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBoxMissions);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBoxMissions);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Interface.augmenteSelection(checkedListBoxMissions);
        }


        private void button19_Click(object sender, EventArgs e)
        {
            Interface.diminueSelection(checkedListBoxMissions);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Interface.topSelection(checkedListBoxMissions);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Interface.downSelection(checkedListBoxMissions);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://clan-fsf.fr/redmine/projects/fsf-server-arma-3/issues");
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ChristopheTdn/FSF-SERVER-ARMA3");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Interface.UnlockFSFServer();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.bistudio.com/showthread.php?161899-FSF-SERVER-Administrate-Multi-dedicated-servers-under-Windows-Environnement");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://community.bistudio.com/wiki/ArmA:_Server_Side_Scripting");

        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Language.ChangeLangage("fr-fr");
            };
            if (radioButton2.Checked)
            {
                Language.ChangeLangage("en-us");
            };
            Application.Restart();
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            bool state = checkBox_ActivatePerformanceTunning.Checked;
            textBox_MaxMsgSend.Enabled = state;
            textBox_MaxSizeGaranteed.Enabled = state;
            textBox_MaxSizeNONGaranteed.Enabled = state;
            textBox_MinimumBandwidth.Enabled = state;
            textBox_MaximumBandwith.Enabled = state;
            textBox_MinErrorToSend.Enabled = state;
            textBox_MaxCustomFileSize.Enabled = state;
            textBox_MinErrorToSendNear.Enabled = state;          
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Interface.SelectionneTous(checkedListBox8);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Interface.InverseSelection(checkedListBox8);
        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void checkBox11_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox22.Enabled = checkBox11.Checked;
        }

        private void comboBox_AILevelPresetRECRUIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_AILevelPresetRECRUIT.SelectedIndex)
            {
                case 0 :
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Value = 0.20M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Enabled = false;
                    break;
                case 1:
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Enabled = false;
                    break;
                case 2:
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Value = 0.8M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Enabled = false;
                    break;
                case 3:
                    Var.fenetrePrincipale.numericUpDown_SkillAIRECRUIT.Enabled = true;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIRECRUIT.Enabled = true;
                    break;
            }
        }

        private void comboBox_AILevelPresetREGULAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_AILevelPresetREGULAR.SelectedIndex)
            {
                case 0:
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Value = 0.20M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Enabled = false;
                    break;
                case 1:
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Enabled = false;
                    break;
                case 2:
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Value = 0.8M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Enabled = false;
                    break;
                case 3:
                    Var.fenetrePrincipale.numericUpDown_SkillAIREGULAR.Enabled = true;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIREGULAR.Enabled = true;
                    break;
            }
        }

        private void comboBox_AILevelPresetVETERAN_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_AILevelPresetVETERAN.SelectedIndex)
            {
                case 0:
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Value = 0.20M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Enabled = false;
                    break;
                case 1:
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Enabled = false;
                    break;
                case 2:
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Value = 0.8M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Enabled = false;
                    break;
                case 3:
                    Var.fenetrePrincipale.numericUpDown_SkillAIVETERAN.Enabled = true;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIVETERAN.Enabled = true;
                    break;
            }
        }

        private void comboBox_AILevelPresetMERCENARY_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox_AILevelPresetMERCENARY.SelectedIndex)
            {
                case 0:
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Value = 0.20M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Enabled = false;
                    break;
                case 1:
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Value = 0.50M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Enabled = false;
                    break;
                case 2:
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Value = 0.8M;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Value = 0.70M;
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Enabled = false;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Enabled = false;
                    break;
                case 3:
                    Var.fenetrePrincipale.numericUpDown_SkillAIMERCENARY.Enabled = true;
                    Var.fenetrePrincipale.numericUpDown_PrecisionAIMERCENARY.Enabled = true;
                    break;
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Network.demarreServeurDistant();
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_proc0_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc1_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc2_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc3_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc4_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc5_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc6_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_proc7_CheckedChanged(object sender, EventArgs e)
        {
            Interface.ActualiseValeurAffinity();
        }

        private void checkBox_Affinity_CheckedChanged(object sender, EventArgs e)
        {
            textBox_ValueAffinity.Enabled = checkBox_Affinity.Checked;
            checkBox_proc0.Enabled = checkBox_Affinity.Checked;
            checkBox_proc1.Enabled = checkBox_Affinity.Checked;
            checkBox_proc2.Enabled = checkBox_Affinity.Checked;
            checkBox_proc3.Enabled = checkBox_Affinity.Checked;
            checkBox_proc4.Enabled = checkBox_Affinity.Checked;
            checkBox_proc5.Enabled = checkBox_Affinity.Checked;
            checkBox_proc6.Enabled = checkBox_Affinity.Checked;
            checkBox_proc7.Enabled = checkBox_Affinity.Checked;

        }

        private void checkBox_HeadLessClientActivate_CheckedChanged(object sender, EventArgs e)
        {

        }



    }
    }

