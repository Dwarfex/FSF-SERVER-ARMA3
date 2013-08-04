﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSF_Server_A3.Classes;
using System.IO;
using System.Net;
using System.ComponentModel;
using WinSCP;

namespace FSF_Server_A3.Classes
{

    class Core
    {
        static public void InitialiseCore()
        {
            if (!Directory.Exists(Var.RepertoireDeSauvegarde))
            {
               // le repertoire n'existe pas
                GestionProfil.CreationNouveauProfil("default");
            }
           GestionProfil.InitialiseListeProfil();
           Var.fenetrePrincipale.comboBox4.SelectedIndex = 0;

           Interface.dessineInterface((Var.fenetrePrincipale.comboBox4.SelectedItem as ComboboxItem).Value.ToString());


        }

        // Divers
        static public string ConversionNomFichierValide(string FileName, char RemplaceChar)
        {
            char[] InvalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (char InvalidFileNameChar in InvalidFileNameChars)
                if (FileName.Contains(InvalidFileNameChar.ToString()))
                    FileName = FileName.Replace(InvalidFileNameChar, RemplaceChar);
            return FileName;
        }

        // Gestion base des registres
       

        static public string GetKeyValue(string DirName, string name)
        { // recupere clé dans la base de registre
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(DirName, true);
                return key.GetValue(name).ToString();
            }
            catch
            {
                return "00";
            } 

        }
        static public void SetKeyValue(string DirName, string name, string value)
        {
            // ecrit clé dans la base de registre
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(DirName);
            key.SetValue(name, value);
        }
       
        // Gestion Action Programme

        static public void LanceServeur(string profil)
        {
            GestionProfil.GenerefichierServeur(profil);
            Var.fenetrePrincipale.button16.Enabled = false;
            new SurveillanceProcess(Var.fenetrePrincipale.textBox18.Text + @"\@FSFServer\" + profil +@"\server.bat", "");  
        }

    // Core
       static public void telechargeFichier(string cheminFichier, string nomFichier)
        {
            WebClient client = new WebClient();
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            // Starts the download
            client.DownloadFileAsync(new Uri(cheminFichier + nomFichier), Var.RepertoireApplication + @"/" + nomFichier);
            Var.fenetrePrincipale.button26.Text = "Téléchargement...";
            Var.fenetrePrincipale.button26.Enabled = false;
        }

       static private void client_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Var.fenetrePrincipale.progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }
       static private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Var.fenetrePrincipale.button26.Visible = false;
            Var.fenetrePrincipale.progressBar1.Visible = false;
            Var.fenetrePrincipale.pictureBox17.Visible = false;
            Var.fenetrePrincipale.button25.Enabled = true;
        }


        // Synchronisation

       /*
*           SYNCHRONISATION      
*/
       #region Synchronisation
       static public void synchro(string typeSynchro)
       {
           try
           {
               string repertoireLocal = Var.fenetrePrincipale.textBox18.Text + @"\@FSF\";
               string repertoireDistant = "/@FSF/";

               Var.fenetrePrincipale.textBox11.Text = "Synchronisation procedure " + typeSynchro + " en cours :" + Environment.NewLine;
               Var.fenetrePrincipale.textBox11.Text += "────────────────────────────" + Environment.NewLine;
               Var.fenetrePrincipale.textBox11.Text += Environment.NewLine + "IMPORTANT : " + Environment.NewLine + "Pour stopper la synchronisation en cours, il vous faut Arreter le processus WinSCP.exe en faisant appel à la combinaison de touche" + Environment.NewLine + " CTRL + MAJ + ESC (onglet processus)." + Environment.NewLine;
               // Setup session options
               SessionOptions sessionOptions = new SessionOptions { };
               switch (typeSynchro)
               {
                   case "beta":
                       SessionOptions sessionOptions1 = new SessionOptions
                       {
                           Protocol = Protocol.Ftp,
                           HostName = "ftp1.clan-fsf.fr",
                           UserName = "fsflauncherA3",
                           Password = "fsflauncherA3"

                       };
                       repertoireDistant = "/@FSF/";
                       sessionOptions = sessionOptions1;

                       break;
               }

               using (Session session = new Session())
               {
                   switch (typeSynchro)
                   {
                       default:
                           Directory.CreateDirectory(repertoireLocal + "@TEMPLATE");
                           Directory.CreateDirectory(repertoireLocal + "@CLIENT");
                           Directory.CreateDirectory(repertoireLocal + "@TEST");
                           Directory.CreateDirectory(repertoireLocal + "@UNITS");
                           Directory.CreateDirectory(repertoireLocal + "@MATERIEL");
                           Directory.CreateDirectory(repertoireLocal + "@ISLANDS");
                           break;
                   }
                   // Will continuously report progress of synchronization
                   session.FileTransferred += FileTransferred;
                   session.FileTransferProgress += FileTransferProgress;

                   // session log
                   //session.DebugLogPath = FSFLauncherCore.cheminARMA3 + @"\userconfig\FSF-LauncherA3\log.txt";
                   // Connect
                   session.Open(sessionOptions);
                   TransferOptions transferOptions = new TransferOptions();
                   transferOptions.TransferMode = TransferMode.Binary;

                   SynchronizationResult synchronizationResult;

                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "****   SYNCHRO @TEMPLATE     ******" + Environment.NewLine);
                   synchronizationResult =
                       session.SynchronizeDirectories(
                           SynchronizationMode.Local,
                           repertoireLocal + "@TEMPLATE",
                           repertoireDistant + "@TEMPLATE",
                           true,
                           false,
                           SynchronizationCriteria.Size);
                   effaceProgressBar();

                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "****   SYNCHRO @CLIENT     ******" + Environment.NewLine);
                   synchronizationResult =
                       session.SynchronizeDirectories(
                           SynchronizationMode.Local,
                           repertoireLocal + "@CLIENT",
                           repertoireDistant + "@CLIENT",
                           true,
                           false,
                           SynchronizationCriteria.Size);
                   effaceProgressBar();

                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "****   SYNCHRO @TEST     ******" + Environment.NewLine);
                   synchronizationResult =
                       session.SynchronizeDirectories(
                           SynchronizationMode.Local,
                           repertoireLocal + "@TEST",
                           repertoireDistant + "@TEST",
                           true,
                           false,
                           SynchronizationCriteria.Size);
                   effaceProgressBar();

                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "****   SYNCHRO @UNITS     ******" + Environment.NewLine);
                   synchronizationResult =
                       session.SynchronizeDirectories(
                           SynchronizationMode.Local,
                           repertoireLocal + "@UNITS",
                           repertoireDistant + "@UNITS",
                           true,
                           false,
                           SynchronizationCriteria.Size);
                   effaceProgressBar();

                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "****   SYNCHRO @MATERIEL     ******" + Environment.NewLine);
                   synchronizationResult =
                       session.SynchronizeDirectories(
                           SynchronizationMode.Local,
                           repertoireLocal + "@MATERIEL",
                           repertoireDistant + "@MATERIEL",
                           true,
                           false,
                           SynchronizationCriteria.Size);
                   effaceProgressBar();

                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "****   SYNCHRO @ISLANDS     ******" + Environment.NewLine);
                   synchronizationResult =
                       session.SynchronizeDirectories(
                           SynchronizationMode.Local,
                           repertoireLocal + "@ISLANDS",
                           repertoireDistant + "@ISLANDS",
                           true,
                           false,
                           SynchronizationCriteria.Size);
                   effaceProgressBar();

                   // Throw on any error
                   synchronizationResult.Check();
                   Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "->fichier " + repertoireLocal + "Organisation.txt mis a jour." + Environment.NewLine);

                   //downloadnouvelleVersion("Organisation.txt", FSFLauncherCore.constCheminFTP + "/@FSF/", FSFLauncherCore.constLoginFTP, FSFLauncherCore.constMdpFTP, repertoireLocal);

               }
           }
           catch (Exception z)
           {
               Var.fenetrePrincipale.textBox11.Text += "Error: " + z;
           }
           Var.fenetrePrincipale.textBox11.AppendText(Environment.NewLine + "_______________" + Environment.NewLine);
           Var.fenetrePrincipale.textBox11.Text += "Fin de la synchro";


       }
       static private void effaceProgressBar()
       {
           Var.fenetrePrincipale.label11.Text = "";
           Var.fenetrePrincipale.label19.Text = "";
           Var.fenetrePrincipale.progressBar2.Value = 0;
           Var.fenetrePrincipale.progressBar3.Value = 0;
       }
       static public void FileTransferred(object sender, TransferEventArgs e)
       {
           if (e.Error == null)
           {
               Var.fenetrePrincipale.textBox11.AppendText(e.FileName + " (ok)" + Environment.NewLine);

           }
           else
           {
               if (e.FileName.IndexOf("FSFLauncher") == 0)
               {
                   Var.fenetrePrincipale.textBox11.AppendText("ERREUR :" + e.FileName + " (" + e.Error + ")" + Environment.NewLine);
               }
               else
               {
                   Var.fenetrePrincipale.textBox11.AppendText("INFO  : Impossible de mettre a jour FSFLauncher.exe quand il est lancé" + Environment.NewLine);
               }
           }
       }
       static public void FileTransferProgress(object sender, FileTransferProgressEventArgs e)
       {

           Var.fenetrePrincipale.label11.Text = ": " + e.FileName.Replace(Var.fenetrePrincipale.textBox18.Text, "");
           Var.fenetrePrincipale.label19.Text = ": " + Path.GetDirectoryName(e.Directory).Replace(Var.fenetrePrincipale.textBox18.Text, "");
           Var.fenetrePrincipale.progressBar2.Value = int.Parse(Math.Truncate(e.FileProgress * 100).ToString());
           Var.fenetrePrincipale.progressBar3.Value = int.Parse(Math.Truncate(e.OverallProgress * 100).ToString());
       }

       #endregion

    }


}
