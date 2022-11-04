namespace PGM
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectWiimoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectJoyconLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectJoyconRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectBothJoyconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectWiimoteJoyconLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectWiimoteJoyconRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectProControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.disconnectWiimoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectJoyconLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectJoyconRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectBothJoyconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectWiimoteJoyconLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectWiimoteJoyconRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectProControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webcamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.inputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.outputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xboxOutputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extraOutputsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.deviceToolStripMenuItem,
            this.processToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectWiimoteToolStripMenuItem,
            this.connectJoyconLeftToolStripMenuItem,
            this.connectJoyconRightToolStripMenuItem,
            this.connectBothJoyconsToolStripMenuItem,
            this.connectWiimoteJoyconLeftToolStripMenuItem,
            this.connectWiimoteJoyconRightToolStripMenuItem,
            this.connectProControllerToolStripMenuItem,
            this.toolStripSeparator2,
            this.disconnectWiimoteToolStripMenuItem,
            this.disconnectJoyconLeftToolStripMenuItem,
            this.disconnectJoyconRightToolStripMenuItem,
            this.disconnectBothJoyconsToolStripMenuItem,
            this.disconnectWiimoteJoyconLeftToolStripMenuItem,
            this.disconnectWiimoteJoyconRightToolStripMenuItem,
            this.disconnectProControllerToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // connectWiimoteToolStripMenuItem
            // 
            this.connectWiimoteToolStripMenuItem.Name = "connectWiimoteToolStripMenuItem";
            this.connectWiimoteToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectWiimoteToolStripMenuItem.Text = "Connect Wiimote";
            this.connectWiimoteToolStripMenuItem.Click += new System.EventHandler(this.connectWiimoteToolStripMenuItem_Click);
            // 
            // connectJoyconLeftToolStripMenuItem
            // 
            this.connectJoyconLeftToolStripMenuItem.Name = "connectJoyconLeftToolStripMenuItem";
            this.connectJoyconLeftToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectJoyconLeftToolStripMenuItem.Text = "Connect Joycon left";
            this.connectJoyconLeftToolStripMenuItem.Click += new System.EventHandler(this.connectJoyconLeftToolStripMenuItem_Click);
            // 
            // connectJoyconRightToolStripMenuItem
            // 
            this.connectJoyconRightToolStripMenuItem.Name = "connectJoyconRightToolStripMenuItem";
            this.connectJoyconRightToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectJoyconRightToolStripMenuItem.Text = "Connect Joycon right";
            this.connectJoyconRightToolStripMenuItem.Click += new System.EventHandler(this.connectJoyconRightToolStripMenuItem_Click);
            // 
            // connectBothJoyconsToolStripMenuItem
            // 
            this.connectBothJoyconsToolStripMenuItem.Name = "connectBothJoyconsToolStripMenuItem";
            this.connectBothJoyconsToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectBothJoyconsToolStripMenuItem.Text = "Connect both Joycons";
            this.connectBothJoyconsToolStripMenuItem.Click += new System.EventHandler(this.connectBothJoyconsToolStripMenuItem_Click);
            // 
            // connectWiimoteJoyconLeftToolStripMenuItem
            // 
            this.connectWiimoteJoyconLeftToolStripMenuItem.Name = "connectWiimoteJoyconLeftToolStripMenuItem";
            this.connectWiimoteJoyconLeftToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectWiimoteJoyconLeftToolStripMenuItem.Text = "Connect Wiimote Joycon left";
            this.connectWiimoteJoyconLeftToolStripMenuItem.Click += new System.EventHandler(this.connectWiimoteJoyconLeftToolStripMenuItem_Click);
            // 
            // connectWiimoteJoyconRightToolStripMenuItem
            // 
            this.connectWiimoteJoyconRightToolStripMenuItem.Name = "connectWiimoteJoyconRightToolStripMenuItem";
            this.connectWiimoteJoyconRightToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectWiimoteJoyconRightToolStripMenuItem.Text = "Connect Wiimote Joycon right";
            this.connectWiimoteJoyconRightToolStripMenuItem.Click += new System.EventHandler(this.connectWiimoteJoyconRightToolStripMenuItem_Click);
            // 
            // connectProControllerToolStripMenuItem
            // 
            this.connectProControllerToolStripMenuItem.Name = "connectProControllerToolStripMenuItem";
            this.connectProControllerToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.connectProControllerToolStripMenuItem.Text = "Connect Pro Controller";
            this.connectProControllerToolStripMenuItem.Click += new System.EventHandler(this.connectProControllerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(246, 6);
            // 
            // disconnectWiimoteToolStripMenuItem
            // 
            this.disconnectWiimoteToolStripMenuItem.Name = "disconnectWiimoteToolStripMenuItem";
            this.disconnectWiimoteToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectWiimoteToolStripMenuItem.Text = "Disconnect Wiimote";
            this.disconnectWiimoteToolStripMenuItem.Click += new System.EventHandler(this.disconnectWiimoteToolStripMenuItem_Click);
            // 
            // disconnectJoyconLeftToolStripMenuItem
            // 
            this.disconnectJoyconLeftToolStripMenuItem.Name = "disconnectJoyconLeftToolStripMenuItem";
            this.disconnectJoyconLeftToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectJoyconLeftToolStripMenuItem.Text = "Disconnect Joycon left";
            this.disconnectJoyconLeftToolStripMenuItem.Click += new System.EventHandler(this.disconnectJoyconLeftToolStripMenuItem_Click);
            // 
            // disconnectJoyconRightToolStripMenuItem
            // 
            this.disconnectJoyconRightToolStripMenuItem.Name = "disconnectJoyconRightToolStripMenuItem";
            this.disconnectJoyconRightToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectJoyconRightToolStripMenuItem.Text = "Disconnect Joycon right";
            this.disconnectJoyconRightToolStripMenuItem.Click += new System.EventHandler(this.disconnectJoyconRightToolStripMenuItem_Click);
            // 
            // disconnectBothJoyconsToolStripMenuItem
            // 
            this.disconnectBothJoyconsToolStripMenuItem.Name = "disconnectBothJoyconsToolStripMenuItem";
            this.disconnectBothJoyconsToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectBothJoyconsToolStripMenuItem.Text = "Disconnect both Joycons";
            this.disconnectBothJoyconsToolStripMenuItem.Click += new System.EventHandler(this.disconnectBothJoyconsToolStripMenuItem_Click);
            // 
            // disconnectWiimoteJoyconLeftToolStripMenuItem
            // 
            this.disconnectWiimoteJoyconLeftToolStripMenuItem.Name = "disconnectWiimoteJoyconLeftToolStripMenuItem";
            this.disconnectWiimoteJoyconLeftToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectWiimoteJoyconLeftToolStripMenuItem.Text = "Disconnect Wiimote Joycon left";
            this.disconnectWiimoteJoyconLeftToolStripMenuItem.Click += new System.EventHandler(this.disconnectWiimoteJoyconLeftToolStripMenuItem_Click);
            // 
            // disconnectWiimoteJoyconRightToolStripMenuItem
            // 
            this.disconnectWiimoteJoyconRightToolStripMenuItem.Name = "disconnectWiimoteJoyconRightToolStripMenuItem";
            this.disconnectWiimoteJoyconRightToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectWiimoteJoyconRightToolStripMenuItem.Text = "Disconnect Wiimote Joycon right";
            this.disconnectWiimoteJoyconRightToolStripMenuItem.Click += new System.EventHandler(this.disconnectWiimoteJoyconRightToolStripMenuItem_Click);
            // 
            // disconnectProControllerToolStripMenuItem
            // 
            this.disconnectProControllerToolStripMenuItem.Name = "disconnectProControllerToolStripMenuItem";
            this.disconnectProControllerToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.disconnectProControllerToolStripMenuItem.Text = "Disconnect Pro Controller";
            this.disconnectProControllerToolStripMenuItem.Click += new System.EventHandler(this.disconnectProControllerToolStripMenuItem_Click);
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripMenuItem,
            this.testToolStripMenuItem});
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.runToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.runToolStripMenuItem.Text = "Run";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            this.testToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.testToolStripMenuItem.Text = "Test";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.webcamToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.inputsToolStripMenuItem,
            this.toolStripSeparator3,
            this.outputsToolStripMenuItem,
            this.xboxOutputsToolStripMenuItem,
            this.extraOutputsToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // webcamToolStripMenuItem
            // 
            this.webcamToolStripMenuItem.Name = "webcamToolStripMenuItem";
            this.webcamToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.webcamToolStripMenuItem.Text = "Webcam Inputs";
            this.webcamToolStripMenuItem.Click += new System.EventHandler(this.webcamToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem3.Text = "Classic Inputs";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem4.Text = "X Inputs";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem5.Text = "Direct Inputs";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // inputsToolStripMenuItem
            // 
            this.inputsToolStripMenuItem.Name = "inputsToolStripMenuItem";
            this.inputsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.inputsToolStripMenuItem.Text = "Extra Inputs";
            this.inputsToolStripMenuItem.Click += new System.EventHandler(this.inputsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(154, 6);
            // 
            // outputsToolStripMenuItem
            // 
            this.outputsToolStripMenuItem.Name = "outputsToolStripMenuItem";
            this.outputsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.outputsToolStripMenuItem.Text = "Classic Outputs";
            this.outputsToolStripMenuItem.Click += new System.EventHandler(this.outputsToolStripMenuItem_Click);
            // 
            // xboxOutputsToolStripMenuItem
            // 
            this.xboxOutputsToolStripMenuItem.Name = "xboxOutputsToolStripMenuItem";
            this.xboxOutputsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.xboxOutputsToolStripMenuItem.Text = "X Outputs";
            this.xboxOutputsToolStripMenuItem.Click += new System.EventHandler(this.xboxOutputsToolStripMenuItem_Click);
            // 
            // extraOutputsToolStripMenuItem
            // 
            this.extraOutputsToolStripMenuItem.Name = "extraOutputsToolStripMenuItem";
            this.extraOutputsToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.extraOutputsToolStripMenuItem.Text = "Extra Outputs";
            this.extraOutputsToolStripMenuItem.Click += new System.EventHandler(this.extraOutputsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem1.Text = "?";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.autocompleteMenu1.SetAutocompleteMenu(this.fastColoredTextBox1, this.autocompleteMenu1);
            this.fastColoredTextBox1.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\r\n^\\s*(case|default)\\s*[^:]" +
    "*(?<range>:)\\s*(?<range>[^;]+);\r\n";
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.fastColoredTextBox1.CharHeight = 14;
            this.fastColoredTextBox1.CharWidth = 8;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Language = FastColoredTextBoxNS.Language.CSharp;
            this.fastColoredTextBox1.LeftBracket = '(';
            this.fastColoredTextBox1.LeftBracket2 = '{';
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 24);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.RightBracket = ')';
            this.fastColoredTextBox1.RightBracket2 = '}';
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
            this.fastColoredTextBox1.Size = new System.Drawing.Size(800, 426);
            this.fastColoredTextBox1.TabIndex = 1;
            this.fastColoredTextBox1.Zoom = 100;
            this.fastColoredTextBox1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fastColoredTextBox1_TextChanged);
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.AllowsTabKey = true;
            this.autocompleteMenu1.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("autocompleteMenu1.Colors")));
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = null;
            this.autocompleteMenu1.Items = new string[] {
        "kmevent",
        "sendinput",
        "pollcount",
        "getstate",
        "Math.Abs",
        "Math.Pow",
        "Math.Sign",
        "keys12345",
        "keys54321",
        "wd",
        "wu",
        "valchanged",
        "Scale",
        "width",
        "height",
        "Key_LBUTTON",
        "Key_RBUTTON",
        "Key_CANCEL",
        "Key_MBUTTON",
        "Key_XBUTTON1",
        "Key_XBUTTON2",
        "Key_BACK",
        "Key_Tab",
        "Key_CLEAR",
        "Key_Return",
        "Key_SHIFT",
        "Key_CONTROL",
        "Key_MENU",
        "Key_PAUSE",
        "Key_CAPITAL",
        "Key_KANA",
        "Key_HANGEUL",
        "Key_HANGUL",
        "Key_JUNJA",
        "Key_FINAL",
        "Key_HANJA",
        "Key_KANJI",
        "Key_Escape",
        "Key_CONVERT",
        "Key_NONCONVERT",
        "Key_ACCEPT",
        "Key_MODECHANGE",
        "Key_Space",
        "Key_PRIOR",
        "Key_NEXT",
        "Key_END",
        "Key_HOME",
        "Key_LEFT",
        "Key_UP",
        "Key_RIGHT",
        "Key_DOWN",
        "Key_SELECT",
        "Key_PRINT",
        "Key_EXECUTE",
        "Key_SNAPSHOT",
        "Key_INSERT",
        "Key_DELETE",
        "Key_HELP",
        "Key_APOSTROPHE",
        "Key_0",
        "Key_1",
        "Key_2",
        "Key_3",
        "Key_4",
        "Key_5",
        "Key_6",
        "Key_7",
        "Key_8",
        "Key_9",
        "Key_A",
        "Key_B",
        "Key_C",
        "Key_D",
        "Key_E",
        "Key_F",
        "Key_G",
        "Key_H",
        "Key_I",
        "Key_J",
        "Key_K",
        "Key_L",
        "Key_M",
        "Key_N",
        "Key_O",
        "Key_P",
        "Key_Q",
        "Key_R",
        "Key_S",
        "Key_T",
        "Key_U",
        "Key_V",
        "Key_W",
        "Key_X",
        "Key_Y",
        "Key_Z",
        "Key_LWIN",
        "Key_RWIN",
        "Key_APPS",
        "Key_SLEEP",
        "Key_NUMPAD0",
        "Key_NUMPAD1",
        "Key_NUMPAD2",
        "Key_NUMPAD3",
        "Key_NUMPAD4",
        "Key_NUMPAD5",
        "Key_NUMPAD6",
        "Key_NUMPAD7",
        "Key_NUMPAD8",
        "Key_NUMPAD9",
        "Key_MULTIPLY",
        "Key_ADD",
        "Key_SEPARATOR",
        "Key_SUBTRACT",
        "Key_DECIMAL",
        "Key_DIVIDE",
        "Key_F1",
        "Key_F2",
        "Key_F3",
        "Key_F4",
        "Key_F5",
        "Key_F6",
        "Key_F7",
        "Key_F8",
        "Key_F9",
        "Key_F10",
        "Key_F11",
        "Key_F12",
        "Key_F13",
        "Key_F14",
        "Key_F15",
        "Key_F16",
        "Key_F17",
        "Key_F18",
        "Key_F19",
        "Key_F20",
        "Key_F21",
        "Key_F22",
        "Key_F23",
        "Key_F24",
        "Key_NUMLOCK",
        "Key_SCROLL",
        "Key_LeftShift",
        "Key_RightShift",
        "Key_LeftControl",
        "Key_RightControl",
        "Key_LMENU",
        "Key_RMENU",
        "Key_BROWSER_BACK",
        "Key_BROWSER_FORWARD",
        "Key_BROWSER_REFRESH",
        "Key_BROWSER_STOP",
        "Key_BROWSER_SEARCH",
        "Key_BROWSER_FAVORITES",
        "Key_BROWSER_HOME",
        "Key_VOLUME_MUTE",
        "Key_VOLUME_DOWN",
        "Key_VOLUME_UP",
        "Key_MEDIA_NEXT_TRACK",
        "Key_MEDIA_PREV_TRACK",
        "Key_MEDIA_STOP",
        "Key_MEDIA_PLAY_PAUSE",
        "Key_LAUNCH_MAIL",
        "Key_LAUNCH_MEDIA_SELECT",
        "Key_LAUNCH_APP1",
        "Key_LAUNCH_APP2",
        "Key_OEM_1",
        "Key_OEM_PLUS",
        "Key_OEM_COMMA",
        "Key_OEM_MINUS",
        "Key_OEM_PERIOD",
        "Key_OEM_2",
        "Key_OEM_3",
        "Key_OEM_4",
        "Key_OEM_5",
        "Key_OEM_6",
        "Key_OEM_7",
        "Key_OEM_8",
        "Key_OEM_102",
        "Key_PROCESSKEY",
        "Key_PACKET",
        "Key_ATTN",
        "Key_CRSEL",
        "Key_EXSEL",
        "Key_EREOF",
        "Key_PLAY",
        "Key_ZOOM",
        "Key_NONAME",
        "Key_PA1",
        "Key_OEM_CLEAR",
        "irx",
        "iry",
        "WiimoteButtonStateA",
        "WiimoteButtonStateB",
        "WiimoteButtonStateMinus",
        "WiimoteButtonStateHome",
        "WiimoteButtonStatePlus",
        "WiimoteButtonStateOne",
        "WiimoteButtonStateTwo",
        "WiimoteButtonStateUp",
        "WiimoteButtonStateDown",
        "WiimoteButtonStateLeft",
        "WiimoteButtonStateRight",
        "WiimoteRawValuesX",
        "WiimoteRawValuesY",
        "WiimoteRawValuesZ",
        "WiimoteNunchuckStateRawJoystickX",
        "WiimoteNunchuckStateRawJoystickY",
        "WiimoteNunchuckStateRawValuesX",
        "WiimoteNunchuckStateRawValuesY",
        "WiimoteNunchuckStateRawValuesZ",
        "WiimoteNunchuckStateC",
        "WiimoteNunchuckStateZ",
        "JoyconRightStickX",
        "JoyconRightStickY",
        "JoyconRightButtonSHOULDER_1",
        "JoyconRightButtonSHOULDER_2",
        "JoyconRightButtonSR",
        "JoyconRightButtonSL",
        "JoyconRightButtonDPAD_DOWN",
        "JoyconRightButtonDPAD_RIGHT",
        "JoyconRightButtonDPAD_UP",
        "JoyconRightButtonDPAD_LEFT",
        "JoyconRightButtonPLUS",
        "JoyconRightButtonHOME",
        "JoyconRightButtonSTICK",
        "JoyconRightButtonACC",
        "JoyconRightButtonSPA",
        "JoyconRightRollLeft",
        "JoyconRightRollRight",
        "JoyconRightAccelX",
        "JoyconRightAccelY",
        "JoyconRightGyroX",
        "JoyconRightGyroY",
        "JoyconRightGyroCenter",
        "JoyconRightAccelCenter",
        "JoyconRightStickCenter",
        "JoyconLeftStickX",
        "JoyconLeftStickY",
        "JoyconLeftButtonSHOULDER_1",
        "JoyconLeftButtonSHOULDER_2",
        "JoyconLeftButtonSR",
        "JoyconLeftButtonSL",
        "JoyconLeftButtonDPAD_DOWN",
        "JoyconLeftButtonDPAD_RIGHT",
        "JoyconLeftButtonDPAD_UP",
        "JoyconLeftButtonDPAD_LEFT",
        "JoyconLeftButtonMINUS",
        "JoyconLeftButtonCAPTURE",
        "JoyconLeftButtonSTICK",
        "JoyconLeftButtonACC",
        "JoyconLeftButtonSMA",
        "JoyconLeftRollLeft",
        "JoyconLeftRollRight",
        "JoyconLeftAccelX",
        "JoyconLeftAccelY",
        "JoyconLeftGyroX",
        "JoyconLeftGyroY",
        "JoyconLeftGyroCenter",
        "JoyconLeftAccelCenter",
        "JoyconLeftStickCenter",
        "ProControllerLeftStickX",
        "ProControllerLeftStickY",
        "ProControllerRightStickX",
        "ProControllerRightStickY",
        "ProControllerButtonSHOULDER_Left_1",
        "ProControllerButtonSHOULDER_Left_2",
        "ProControllerButtonDPAD_DOWN",
        "ProControllerButtonDPAD_RIGHT",
        "ProControllerButtonDPAD_UP",
        "ProControllerButtonDPAD_LEFT",
        "ProControllerButtonMINUS",
        "ProControllerButtonCAPTURE",
        "ProControllerButtonSTICK_Left",
        "ProControllerButtonSHOULDER_Right_1",
        "ProControllerButtonSHOULDER_Right_2",
        "ProControllerButtonA",
        "ProControllerButtonB",
        "ProControllerButtonX",
        "ProControllerButtonY",
        "ProControllerButtonPLUS",
        "ProControllerButtonHOME",
        "ProControllerButtonSTICK_Right",
        "ProControllerAccelX",
        "ProControllerAccelY",
        "ProControllerGyroX",
        "ProControllerGyroY",
        "ProControllerGyroCenter",
        "ProControllerAccelCenter",
        "ProControllerStickCenter",
        "camx",
        "camy",
        "Controller1ButtonAPressed",
        "Controller2ButtonAPressed",
        "Controller1ButtonBPressed",
        "Controller2ButtonBPressed",
        "Controller1ButtonXPressed",
        "Controller2ButtonXPressed",
        "Controller1ButtonYPressed",
        "Controller2ButtonYPressed",
        "Controller1ButtonStartPressed",
        "Controller2ButtonStartPressed",
        "Controller1ButtonBackPressed",
        "Controller2ButtonBackPressed",
        "Controller1ButtonDownPressed",
        "Controller2ButtonDownPressed",
        "Controller1ButtonUpPressed",
        "Controller2ButtonUpPressed",
        "Controller1ButtonLeftPressed",
        "Controller2ButtonLeftPressed",
        "Controller1ButtonRightPressed",
        "Controller2ButtonRightPressed",
        "Controller1ButtonShoulderLeftPressed",
        "Controller2ButtonShoulderLeftPressed",
        "Controller1ButtonShoulderRightPressed",
        "Controller2ButtonShoulderRightPressed",
        "Controller1ThumbpadLeftPressed",
        "Controller2ThumbpadLeftPressed",
        "Controller1ThumbpadRightPressed",
        "Controller2ThumbpadRightPressed",
        "Controller1TriggerLeftPosition",
        "Controller2TriggerLeftPosition",
        "Controller1TriggerRightPosition",
        "Controller2TriggerRightPosition",
        "Controller1ThumbLeftX",
        "Controller2ThumbLeftX",
        "Controller1ThumbLeftY",
        "Controller2ThumbLeftY",
        "Controller1ThumbRightX",
        "Controller2ThumbRightX",
        "Controller1ThumbRightY",
        "Controller2ThumbRightY",
        "Controller1GyroX",
        "Controller2GyroX",
        "Controller1GyroY",
        "Controller2GyroY",
        "Controller1GyroCenter",
        "Controller2GyroCenter",
        "Joystick1AxisX",
        "Joystick1AxisY",
        "Joystick1AxisZ",
        "Joystick1RotationX",
        "Joystick1RotationY",
        "Joystick1RotationZ",
        "Joystick1Sliders0",
        "Joystick1Sliders1",
        "Joystick1PointOfViewControllers0",
        "Joystick1PointOfViewControllers1",
        "Joystick1PointOfViewControllers2",
        "Joystick1PointOfViewControllers3",
        "Joystick1VelocityX",
        "Joystick1VelocityY",
        "Joystick1VelocityZ",
        "Joystick1AngularVelocityX",
        "Joystick1AngularVelocityY",
        "Joystick1AngularVelocityZ",
        "Joystick1VelocitySliders0",
        "Joystick1VelocitySliders1",
        "Joystick1AccelerationX",
        "Joystick1AccelerationY",
        "Joystick1AccelerationZ",
        "Joystick1AngularAccelerationX",
        "Joystick1AngularAccelerationY",
        "Joystick1AngularAccelerationZ",
        "Joystick1AccelerationSliders0",
        "Joystick1AccelerationSliders1",
        "Joystick1ForceX",
        "Joystick1ForceY",
        "Joystick1ForceZ",
        "Joystick1TorqueX",
        "Joystick1TorqueY",
        "Joystick1TorqueZ",
        "Joystick1ForceSliders0",
        "Joystick1ForceSliders1",
        "Joystick1Buttons0",
        "Joystick1Buttons1",
        "Joystick1Buttons2",
        "Joystick1Buttons3",
        "Joystick1Buttons4",
        "Joystick1Buttons5",
        "Joystick1Buttons6",
        "Joystick1Buttons7",
        "Joystick1Buttons8",
        "Joystick1Buttons9",
        "Joystick1Buttons10",
        "Joystick1Buttons11",
        "Joystick1Buttons12",
        "Joystick1Buttons13",
        "Joystick1Buttons14",
        "Joystick1Buttons15",
        "Joystick1Buttons16",
        "Joystick1Buttons17",
        "Joystick1Buttons18",
        "Joystick1Buttons19",
        "Joystick1Buttons20",
        "Joystick1Buttons21",
        "Joystick1Buttons22",
        "Joystick1Buttons23",
        "Joystick1Buttons24",
        "Joystick1Buttons25",
        "Joystick1Buttons26",
        "Joystick1Buttons27",
        "Joystick1Buttons28",
        "Joystick1Buttons29",
        "Joystick1Buttons30",
        "Joystick1Buttons31",
        "Joystick1Buttons32",
        "Joystick1Buttons33",
        "Joystick1Buttons34",
        "Joystick1Buttons35",
        "Joystick1Buttons36",
        "Joystick1Buttons37",
        "Joystick1Buttons38",
        "Joystick1Buttons39",
        "Joystick1Buttons40",
        "Joystick1Buttons41",
        "Joystick1Buttons42",
        "Joystick1Buttons43",
        "Joystick1Buttons44",
        "Joystick1Buttons45",
        "Joystick1Buttons46",
        "Joystick1Buttons47",
        "Joystick1Buttons48",
        "Joystick1Buttons49",
        "Joystick1Buttons50",
        "Joystick1Buttons51",
        "Joystick1Buttons52",
        "Joystick1Buttons53",
        "Joystick1Buttons54",
        "Joystick1Buttons55",
        "Joystick1Buttons56",
        "Joystick1Buttons57",
        "Joystick1Buttons58",
        "Joystick1Buttons59",
        "Joystick1Buttons60",
        "Joystick1Buttons61",
        "Joystick1Buttons62",
        "Joystick1Buttons63",
        "Joystick1Buttons64",
        "Joystick1Buttons65",
        "Joystick1Buttons66",
        "Joystick1Buttons67",
        "Joystick1Buttons68",
        "Joystick1Buttons69",
        "Joystick1Buttons70",
        "Joystick1Buttons71",
        "Joystick1Buttons72",
        "Joystick1Buttons73",
        "Joystick1Buttons74",
        "Joystick1Buttons75",
        "Joystick1Buttons76",
        "Joystick1Buttons77",
        "Joystick1Buttons78",
        "Joystick1Buttons79",
        "Joystick1Buttons80",
        "Joystick1Buttons81",
        "Joystick1Buttons82",
        "Joystick1Buttons83",
        "Joystick1Buttons84",
        "Joystick1Buttons85",
        "Joystick1Buttons86",
        "Joystick1Buttons87",
        "Joystick1Buttons88",
        "Joystick1Buttons89",
        "Joystick1Buttons90",
        "Joystick1Buttons91",
        "Joystick1Buttons92",
        "Joystick1Buttons93",
        "Joystick1Buttons94",
        "Joystick1Buttons95",
        "Joystick1Buttons96",
        "Joystick1Buttons97",
        "Joystick1Buttons98",
        "Joystick1Buttons99",
        "Joystick1Buttons100",
        "Joystick1Buttons101",
        "Joystick1Buttons102",
        "Joystick1Buttons103",
        "Joystick1Buttons104",
        "Joystick1Buttons105",
        "Joystick1Buttons106",
        "Joystick1Buttons107",
        "Joystick1Buttons108",
        "Joystick1Buttons109",
        "Joystick1Buttons110",
        "Joystick1Buttons111",
        "Joystick1Buttons112",
        "Joystick1Buttons113",
        "Joystick1Buttons114",
        "Joystick1Buttons115",
        "Joystick1Buttons116",
        "Joystick1Buttons117",
        "Joystick1Buttons118",
        "Joystick1Buttons119",
        "Joystick1Buttons120",
        "Joystick1Buttons121",
        "Joystick1Buttons122",
        "Joystick1Buttons123",
        "Joystick1Buttons124",
        "Joystick1Buttons125",
        "Joystick1Buttons126",
        "Joystick1Buttons127",
        "Joystick2AxisX",
        "Joystick2AxisY",
        "Joystick2AxisZ",
        "Joystick2RotationX",
        "Joystick2RotationY",
        "Joystick2RotationZ",
        "Joystick2Sliders0",
        "Joystick2Sliders1",
        "Joystick2PointOfViewControllers0",
        "Joystick2PointOfViewControllers1",
        "Joystick2PointOfViewControllers2",
        "Joystick2PointOfViewControllers3",
        "Joystick2VelocityX",
        "Joystick2VelocityY",
        "Joystick2VelocityZ",
        "Joystick2AngularVelocityX",
        "Joystick2AngularVelocityY",
        "Joystick2AngularVelocityZ",
        "Joystick2VelocitySliders0",
        "Joystick2VelocitySliders1",
        "Joystick2AccelerationX",
        "Joystick2AccelerationY",
        "Joystick2AccelerationZ",
        "Joystick2AngularAccelerationX",
        "Joystick2AngularAccelerationY",
        "Joystick2AngularAccelerationZ",
        "Joystick2AccelerationSliders0",
        "Joystick2AccelerationSliders1",
        "Joystick2ForceX",
        "Joystick2ForceY",
        "Joystick2ForceZ",
        "Joystick2TorqueX",
        "Joystick2TorqueY",
        "Joystick2TorqueZ",
        "Joystick2ForceSliders0",
        "Joystick2ForceSliders1",
        "Joystick2Buttons0",
        "Joystick2Buttons1",
        "Joystick2Buttons2",
        "Joystick2Buttons3",
        "Joystick2Buttons4",
        "Joystick2Buttons5",
        "Joystick2Buttons6",
        "Joystick2Buttons7",
        "Joystick2Buttons8",
        "Joystick2Buttons9",
        "Joystick2Buttons10",
        "Joystick2Buttons11",
        "Joystick2Buttons12",
        "Joystick2Buttons13",
        "Joystick2Buttons14",
        "Joystick2Buttons15",
        "Joystick2Buttons16",
        "Joystick2Buttons17",
        "Joystick2Buttons18",
        "Joystick2Buttons19",
        "Joystick2Buttons20",
        "Joystick2Buttons21",
        "Joystick2Buttons22",
        "Joystick2Buttons23",
        "Joystick2Buttons24",
        "Joystick2Buttons25",
        "Joystick2Buttons26",
        "Joystick2Buttons27",
        "Joystick2Buttons28",
        "Joystick2Buttons29",
        "Joystick2Buttons30",
        "Joystick2Buttons31",
        "Joystick2Buttons32",
        "Joystick2Buttons33",
        "Joystick2Buttons34",
        "Joystick2Buttons35",
        "Joystick2Buttons36",
        "Joystick2Buttons37",
        "Joystick2Buttons38",
        "Joystick2Buttons39",
        "Joystick2Buttons40",
        "Joystick2Buttons41",
        "Joystick2Buttons42",
        "Joystick2Buttons43",
        "Joystick2Buttons44",
        "Joystick2Buttons45",
        "Joystick2Buttons46",
        "Joystick2Buttons47",
        "Joystick2Buttons48",
        "Joystick2Buttons49",
        "Joystick2Buttons50",
        "Joystick2Buttons51",
        "Joystick2Buttons52",
        "Joystick2Buttons53",
        "Joystick2Buttons54",
        "Joystick2Buttons55",
        "Joystick2Buttons56",
        "Joystick2Buttons57",
        "Joystick2Buttons58",
        "Joystick2Buttons59",
        "Joystick2Buttons60",
        "Joystick2Buttons61",
        "Joystick2Buttons62",
        "Joystick2Buttons63",
        "Joystick2Buttons64",
        "Joystick2Buttons65",
        "Joystick2Buttons66",
        "Joystick2Buttons67",
        "Joystick2Buttons68",
        "Joystick2Buttons69",
        "Joystick2Buttons70",
        "Joystick2Buttons71",
        "Joystick2Buttons72",
        "Joystick2Buttons73",
        "Joystick2Buttons74",
        "Joystick2Buttons75",
        "Joystick2Buttons76",
        "Joystick2Buttons77",
        "Joystick2Buttons78",
        "Joystick2Buttons79",
        "Joystick2Buttons80",
        "Joystick2Buttons81",
        "Joystick2Buttons82",
        "Joystick2Buttons83",
        "Joystick2Buttons84",
        "Joystick2Buttons85",
        "Joystick2Buttons86",
        "Joystick2Buttons87",
        "Joystick2Buttons88",
        "Joystick2Buttons89",
        "Joystick2Buttons90",
        "Joystick2Buttons91",
        "Joystick2Buttons92",
        "Joystick2Buttons93",
        "Joystick2Buttons94",
        "Joystick2Buttons95",
        "Joystick2Buttons96",
        "Joystick2Buttons97",
        "Joystick2Buttons98",
        "Joystick2Buttons99",
        "Joystick2Buttons100",
        "Joystick2Buttons101",
        "Joystick2Buttons102",
        "Joystick2Buttons103",
        "Joystick2Buttons104",
        "Joystick2Buttons105",
        "Joystick2Buttons106",
        "Joystick2Buttons107",
        "Joystick2Buttons108",
        "Joystick2Buttons109",
        "Joystick2Buttons110",
        "Joystick2Buttons111",
        "Joystick2Buttons112",
        "Joystick2Buttons113",
        "Joystick2Buttons114",
        "Joystick2Buttons115",
        "Joystick2Buttons116",
        "Joystick2Buttons117",
        "Joystick2Buttons118",
        "Joystick2Buttons119",
        "Joystick2Buttons120",
        "Joystick2Buttons121",
        "Joystick2Buttons122",
        "Joystick2Buttons123",
        "Joystick2Buttons124",
        "Joystick2Buttons125",
        "Joystick2Buttons126",
        "Joystick2Buttons127",
        "Mouse1Buttons0",
        "Mouse1Buttons1",
        "Mouse1Buttons2",
        "Mouse1Buttons3",
        "Mouse1Buttons4",
        "Mouse1Buttons5",
        "Mouse1Buttons6",
        "Mouse1Buttons7",
        "Mouse1AxisX",
        "Mouse1AxisY",
        "Mouse1AxisZ",
        "Mouse2Buttons0",
        "Mouse2Buttons1",
        "Mouse2Buttons2",
        "Mouse2Buttons3",
        "Mouse2Buttons4",
        "Mouse2Buttons5",
        "Mouse2Buttons6",
        "Mouse2Buttons7",
        "Mouse2AxisX",
        "Mouse2AxisY",
        "Mouse2AxisZ",
        "MouseHookX",
        "MouseHookY",
        "Keyboard1KeyEscape",
        "Keyboard1KeyD1",
        "Keyboard1KeyD2",
        "Keyboard1KeyD3",
        "Keyboard1KeyD4",
        "Keyboard1KeyD5",
        "Keyboard1KeyD6",
        "Keyboard1KeyD7",
        "Keyboard1KeyD8",
        "Keyboard1KeyD9",
        "Keyboard1KeyD0",
        "Keyboard1KeyMinus",
        "Keyboard1KeyEquals",
        "Keyboard1KeyBack",
        "Keyboard1KeyTab",
        "Keyboard1KeyQ",
        "Keyboard1KeyW",
        "Keyboard1KeyE",
        "Keyboard1KeyR",
        "Keyboard1KeyT",
        "Keyboard1KeyY",
        "Keyboard1KeyU",
        "Keyboard1KeyI",
        "Keyboard1KeyO",
        "Keyboard1KeyP",
        "Keyboard1KeyLeftBracket",
        "Keyboard1KeyRightBracket",
        "Keyboard1KeyReturn",
        "Keyboard1KeyLeftControl",
        "Keyboard1KeyA",
        "Keyboard1KeyS",
        "Keyboard1KeyD",
        "Keyboard1KeyF",
        "Keyboard1KeyG",
        "Keyboard1KeyH",
        "Keyboard1KeyJ",
        "Keyboard1KeyK",
        "Keyboard1KeyL",
        "Keyboard1KeySemicolon",
        "Keyboard1KeyApostrophe",
        "Keyboard1KeyGrave",
        "Keyboard1KeyLeftShift",
        "Keyboard1KeyBackslash",
        "Keyboard1KeyZ",
        "Keyboard1KeyX",
        "Keyboard1KeyC",
        "Keyboard1KeyV",
        "Keyboard1KeyB",
        "Keyboard1KeyN",
        "Keyboard1KeyM",
        "Keyboard1KeyComma",
        "Keyboard1KeyPeriod",
        "Keyboard1KeySlash",
        "Keyboard1KeyRightShift",
        "Keyboard1KeyMultiply",
        "Keyboard1KeyLeftAlt",
        "Keyboard1KeySpace",
        "Keyboard1KeyCapital",
        "Keyboard1KeyF1",
        "Keyboard1KeyF2",
        "Keyboard1KeyF3",
        "Keyboard1KeyF4",
        "Keyboard1KeyF5",
        "Keyboard1KeyF6",
        "Keyboard1KeyF7",
        "Keyboard1KeyF8",
        "Keyboard1KeyF9",
        "Keyboard1KeyF10",
        "Keyboard1KeyNumberLock",
        "Keyboard1KeyScrollLock",
        "Keyboard1KeyNumberPad7",
        "Keyboard1KeyNumberPad8",
        "Keyboard1KeyNumberPad9",
        "Keyboard1KeySubtract",
        "Keyboard1KeyNumberPad4",
        "Keyboard1KeyNumberPad5",
        "Keyboard1KeyNumberPad6",
        "Keyboard1KeyAdd",
        "Keyboard1KeyNumberPad1",
        "Keyboard1KeyNumberPad2",
        "Keyboard1KeyNumberPad3",
        "Keyboard1KeyNumberPad0",
        "Keyboard1KeyDecimal",
        "Keyboard1KeyOem102",
        "Keyboard1KeyF11",
        "Keyboard1KeyF12",
        "Keyboard1KeyF13",
        "Keyboard1KeyF14",
        "Keyboard1KeyF15",
        "Keyboard1KeyKana",
        "Keyboard1KeyAbntC1",
        "Keyboard1KeyConvert",
        "Keyboard1KeyNoConvert",
        "Keyboard1KeyYen",
        "Keyboard1KeyAbntC2",
        "Keyboard1KeyNumberPadEquals",
        "Keyboard1KeyPreviousTrack",
        "Keyboard1KeyAT",
        "Keyboard1KeyColon",
        "Keyboard1KeyUnderline",
        "Keyboard1KeyKanji",
        "Keyboard1KeyStop",
        "Keyboard1KeyAX",
        "Keyboard1KeyUnlabeled",
        "Keyboard1KeyNextTrack",
        "Keyboard1KeyNumberPadEnter",
        "Keyboard1KeyRightControl",
        "Keyboard1KeyMute",
        "Keyboard1KeyCalculator",
        "Keyboard1KeyPlayPause",
        "Keyboard1KeyMediaStop",
        "Keyboard1KeyVolumeDown",
        "Keyboard1KeyVolumeUp",
        "Keyboard1KeyWebHome",
        "Keyboard1KeyNumberPadComma",
        "Keyboard1KeyDivide",
        "Keyboard1KeyPrintScreen",
        "Keyboard1KeyRightAlt",
        "Keyboard1KeyPause",
        "Keyboard1KeyHome",
        "Keyboard1KeyUp",
        "Keyboard1KeyPageUp",
        "Keyboard1KeyLeft",
        "Keyboard1KeyRight",
        "Keyboard1KeyEnd",
        "Keyboard1KeyDown",
        "Keyboard1KeyPageDown",
        "Keyboard1KeyInsert",
        "Keyboard1KeyDelete",
        "Keyboard1KeyLeftWindowsKey",
        "Keyboard1KeyRightWindowsKey",
        "Keyboard1KeyApplications",
        "Keyboard1KeyPower",
        "Keyboard1KeySleep",
        "Keyboard1KeyWake",
        "Keyboard1KeyWebSearch",
        "Keyboard1KeyWebFavorites",
        "Keyboard1KeyWebRefresh",
        "Keyboard1KeyWebStop",
        "Keyboard1KeyWebForward",
        "Keyboard1KeyWebBack",
        "Keyboard1KeyMyComputer",
        "Keyboard1KeyMail",
        "Keyboard1KeyMediaSelect",
        "Keyboard1KeyUnknown",
        "Keyboard2KeyEscape",
        "Keyboard2KeyD1",
        "Keyboard2KeyD2",
        "Keyboard2KeyD3",
        "Keyboard2KeyD4",
        "Keyboard2KeyD5",
        "Keyboard2KeyD6",
        "Keyboard2KeyD7",
        "Keyboard2KeyD8",
        "Keyboard2KeyD9",
        "Keyboard2KeyD0",
        "Keyboard2KeyMinus",
        "Keyboard2KeyEquals",
        "Keyboard2KeyBack",
        "Keyboard2KeyTab",
        "Keyboard2KeyQ",
        "Keyboard2KeyW",
        "Keyboard2KeyE",
        "Keyboard2KeyR",
        "Keyboard2KeyT",
        "Keyboard2KeyY",
        "Keyboard2KeyU",
        "Keyboard2KeyI",
        "Keyboard2KeyO",
        "Keyboard2KeyP",
        "Keyboard2KeyLeftBracket",
        "Keyboard2KeyRightBracket",
        "Keyboard2KeyReturn",
        "Keyboard2KeyLeftControl",
        "Keyboard2KeyA",
        "Keyboard2KeyS",
        "Keyboard2KeyD",
        "Keyboard2KeyF",
        "Keyboard2KeyG",
        "Keyboard2KeyH",
        "Keyboard2KeyJ",
        "Keyboard2KeyK",
        "Keyboard2KeyL",
        "Keyboard2KeySemicolon",
        "Keyboard2KeyApostrophe",
        "Keyboard2KeyGrave",
        "Keyboard2KeyLeftShift",
        "Keyboard2KeyBackslash",
        "Keyboard2KeyZ",
        "Keyboard2KeyX",
        "Keyboard2KeyC",
        "Keyboard2KeyV",
        "Keyboard2KeyB",
        "Keyboard2KeyN",
        "Keyboard2KeyM",
        "Keyboard2KeyComma",
        "Keyboard2KeyPeriod",
        "Keyboard2KeySlash",
        "Keyboard2KeyRightShift",
        "Keyboard2KeyMultiply",
        "Keyboard2KeyLeftAlt",
        "Keyboard2KeySpace",
        "Keyboard2KeyCapital",
        "Keyboard2KeyF1",
        "Keyboard2KeyF2",
        "Keyboard2KeyF3",
        "Keyboard2KeyF4",
        "Keyboard2KeyF5",
        "Keyboard2KeyF6",
        "Keyboard2KeyF7",
        "Keyboard2KeyF8",
        "Keyboard2KeyF9",
        "Keyboard2KeyF10",
        "Keyboard2KeyNumberLock",
        "Keyboard2KeyScrollLock",
        "Keyboard2KeyNumberPad7",
        "Keyboard2KeyNumberPad8",
        "Keyboard2KeyNumberPad9",
        "Keyboard2KeySubtract",
        "Keyboard2KeyNumberPad4",
        "Keyboard2KeyNumberPad5",
        "Keyboard2KeyNumberPad6",
        "Keyboard2KeyAdd",
        "Keyboard2KeyNumberPad1",
        "Keyboard2KeyNumberPad2",
        "Keyboard2KeyNumberPad3",
        "Keyboard2KeyNumberPad0",
        "Keyboard2KeyDecimal",
        "Keyboard2KeyOem102",
        "Keyboard2KeyF11",
        "Keyboard2KeyF12",
        "Keyboard2KeyF13",
        "Keyboard2KeyF14",
        "Keyboard2KeyF15",
        "Keyboard2KeyKana",
        "Keyboard2KeyAbntC1",
        "Keyboard2KeyConvert",
        "Keyboard2KeyNoConvert",
        "Keyboard2KeyYen",
        "Keyboard2KeyAbntC2",
        "Keyboard2KeyNumberPadEquals",
        "Keyboard2KeyPreviousTrack",
        "Keyboard2KeyAT",
        "Keyboard2KeyColon",
        "Keyboard2KeyUnderline",
        "Keyboard2KeyKanji",
        "Keyboard2KeyStop",
        "Keyboard2KeyAX",
        "Keyboard2KeyUnlabeled",
        "Keyboard2KeyNextTrack",
        "Keyboard2KeyNumberPadEnter",
        "Keyboard2KeyRightControl",
        "Keyboard2KeyMute",
        "Keyboard2KeyCalculator",
        "Keyboard2KeyPlayPause",
        "Keyboard2KeyMediaStop",
        "Keyboard2KeyVolumeDown",
        "Keyboard2KeyVolumeUp",
        "Keyboard2KeyWebHome",
        "Keyboard2KeyNumberPadComma",
        "Keyboard2KeyDivide",
        "Keyboard2KeyPrintScreen",
        "Keyboard2KeyRightAlt",
        "Keyboard2KeyPause",
        "Keyboard2KeyHome",
        "Keyboard2KeyUp",
        "Keyboard2KeyPageUp",
        "Keyboard2KeyLeft",
        "Keyboard2KeyRight",
        "Keyboard2KeyEnd",
        "Keyboard2KeyDown",
        "Keyboard2KeyPageDown",
        "Keyboard2KeyInsert",
        "Keyboard2KeyDelete",
        "Keyboard2KeyLeftWindowsKey",
        "Keyboard2KeyRightWindowsKey",
        "Keyboard2KeyApplications",
        "Keyboard2KeyPower",
        "Keyboard2KeySleep",
        "Keyboard2KeyWake",
        "Keyboard2KeyWebSearch",
        "Keyboard2KeyWebFavorites",
        "Keyboard2KeyWebRefresh",
        "Keyboard2KeyWebStop",
        "Keyboard2KeyWebForward",
        "Keyboard2KeyWebBack",
        "Keyboard2KeyMyComputer",
        "Keyboard2KeyMail",
        "Keyboard2KeyMediaSelect",
        "Keyboard2KeyUnknown",
        "TextFromSpeech",
        "UsersAllowedList",
        "sleeptime",
        "KeyboardMouseDriverType",
        "MouseMoveX",
        "MouseMoveY",
        "MouseAbsX",
        "MouseAbsY",
        "MouseDesktopX",
        "MouseDesktopY",
        "SendLeftClick",
        "SendRightClick",
        "SendMiddleClick",
        "SendWheelUp",
        "SendWheelDown",
        "SendLeft",
        "SendRight",
        "SendUp",
        "SendDown",
        "SendLButton",
        "SendRButton",
        "SendCancel",
        "SendMBUTTON",
        "SendXBUTTON1",
        "SendXBUTTON2",
        "SendBack",
        "SendTab",
        "SendClear",
        "SendReturn",
        "SendSHIFT",
        "SendCONTROL",
        "SendMENU",
        "SendPAUSE",
        "SendCAPITAL",
        "SendKANA",
        "SendHANGEUL",
        "SendHANGUL",
        "SendJUNJA",
        "SendFINAL",
        "SendHANJA",
        "SendKANJI",
        "SendEscape",
        "SendCONVERT",
        "SendNONCONVERT",
        "SendACCEPT",
        "SendMODECHANGE",
        "SendSpace",
        "SendPRIOR",
        "SendNEXT",
        "SendEND",
        "SendHOME",
        "SendLEFT",
        "SendUP",
        "SendRIGHT",
        "SendDOWN",
        "SendSELECT",
        "SendPRINT",
        "SendEXECUTE",
        "SendSNAPSHOT",
        "SendINSERT",
        "SendDELETE",
        "SendHELP",
        "SendAPOSTROPHE",
        "Send0",
        "Send1",
        "Send2",
        "Send3",
        "Send4",
        "Send5",
        "Send6",
        "Send7",
        "Send8",
        "Send9",
        "SendA",
        "SendB",
        "SendC",
        "SendD",
        "SendE",
        "SendF",
        "SendG",
        "SendH",
        "SendI",
        "SendJ",
        "SendK",
        "SendL",
        "SendM",
        "SendN",
        "SendO",
        "SendP",
        "SendQ",
        "SendR",
        "SendS",
        "SendT",
        "SendU",
        "SendV",
        "SendW",
        "SendX",
        "SendY",
        "SendZ",
        "SendLWIN",
        "SendRWIN",
        "SendAPPS",
        "SendSLEEP",
        "SendNUMPAD0",
        "SendNUMPAD1",
        "SendNUMPAD2",
        "SendNUMPAD3",
        "SendNUMPAD4",
        "SendNUMPAD5",
        "SendNUMPAD6",
        "SendNUMPAD7",
        "SendNUMPAD8",
        "SendNUMPAD9",
        "SendMULTIPLY",
        "SendADD",
        "SendSEPARATOR",
        "SendSUBTRACT",
        "SendDECIMAL",
        "SendDIVIDE",
        "SendF1",
        "SendF2",
        "SendF3",
        "SendF4",
        "SendF5",
        "SendF6",
        "SendF7",
        "SendF8",
        "SendF9",
        "SendF10",
        "SendF11",
        "SendF12",
        "SendF13",
        "SendF14",
        "SendF15",
        "SendF16",
        "SendF17",
        "SendF18",
        "SendF19",
        "SendF20",
        "SendF21",
        "SendF22",
        "SendF23",
        "SendF24",
        "SendNUMLOCK",
        "SendSCROLL",
        "SendLeftShift",
        "SendRightShift",
        "SendLeftControl",
        "SendRightControl",
        "SendLMENU",
        "SendRMENU",
        "centery",
        "irmode",
        "gyromode",
        "SpeechToText",
        "controller1_send_back",
        "controller1_send_start",
        "controller1_send_A",
        "controller1_send_B",
        "controller1_send_X",
        "controller1_send_Y",
        "controller1_send_up",
        "controller1_send_left",
        "controller1_send_down",
        "controller1_send_right",
        "controller1_send_leftstick",
        "controller1_send_rightstick",
        "controller1_send_leftbumper",
        "controller1_send_rightbumper",
        "controller1_send_lefttrigger",
        "controller1_send_righttrigger",
        "controller1_send_leftstickx",
        "controller1_send_leftsticky",
        "controller1_send_rightstickx",
        "controller1_send_rightsticky",
        "controller1_send_lefttriggerposition",
        "controller1_send_righttriggerposition",
        "controller2_send_back",
        "controller2_send_start",
        "controller2_send_A",
        "controller2_send_B",
        "controller2_send_X",
        "controller2_send_Y",
        "controller2_send_up",
        "controller2_send_left",
        "controller2_send_down",
        "controller2_send_right",
        "controller2_send_leftstick",
        "controller2_send_rightstick",
        "controller2_send_leftbumper",
        "controller2_send_rightbumper",
        "controller2_send_lefttrigger",
        "controller2_send_righttrigger",
        "controller2_send_leftstickx",
        "controller2_send_leftsticky",
        "controller2_send_rightstickx",
        "controller2_send_rightsticky",
        "controller2_send_lefttriggerposition",
        "controller2_send_righttriggerposition",
        "EnableKM",
        "EnableXC",
        "EnableRI",
        "EnableDI",
        "EnableXI",
        "EnableJI",
        "EnablePI",
        "mousexp",
        "mouseyp",
        "testdouble",
        "testbool",
        "PS5ControllerGyroCenter",
        "PS5ControllerAccelCenter",
        "PS5ControllerLeftStickX",
        "PS5ControllerLeftStickY",
        "PS5ControllerRightStickX",
        "PS5ControllerRightStickY",
        "PS5ControllerLeftTriggerPosition",
        "PS5ControllerRightTriggerPosition",
        "PS5ControllerTouchX",
        "PS5ControllerTouchY",
        "PS5ControllerTouchOn",
        "PS5ControllerGyroX",
        "PS5ControllerGyroY",
        "PS5ControllerAccelX",
        "PS5ControllerAccelY",
        "PS5ControllerButtonCrossPressed",
        "PS5ControllerButtonCirclePressed",
        "PS5ControllerButtonSquarePressed",
        "PS5ControllerButtonTrianglePressed",
        "PS5ControllerButtonDPadUpPressed",
        "PS5ControllerButtonDPadRightPressed",
        "PS5ControllerButtonDPadDownPressed",
        "PS5ControllerButtonDPadLeftPressed",
        "PS5ControllerButtonL1Pressed",
        "PS5ControllerButtonR1Pressed",
        "PS5ControllerButtonL2Pressed",
        "PS5ControllerButtonR2Pressed",
        "PS5ControllerButtonL3Pressed",
        "PS5ControllerButtonR3Pressed",
        "PS5ControllerButtonCreatePressed",
        "PS5ControllerButtonMenuPressed",
        "PS5ControllerButtonLogoPressed",
        "PS5ControllerButtonTouchpadPressed",
        "PS5ControllerButtonMicPressed"};
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.fastColoredTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProGamingMapper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem webcamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inputsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem outputsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem xboxOutputsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extraOutputsToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem connectWiimoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectJoyconLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectJoyconRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem disconnectWiimoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectJoyconLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectJoyconRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectBothJoyconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectWiimoteJoyconLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectWiimoteJoyconRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectBothJoyconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectWiimoteJoyconLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectWiimoteJoyconRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectProControllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectProControllerToolStripMenuItem;
    }
}

