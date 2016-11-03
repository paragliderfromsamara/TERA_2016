namespace TERA_2016.measureForms
{
    partial class manualMeasureForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.voltageComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startMeasureBut = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.measureTimesLbl = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cycleTimes = new System.Windows.Forms.NumericUpDown();
            this.averagingTimes = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.dischargeDelay = new System.Windows.Forms.NumericUpDown();
            this.isCyclicMeasure = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.polarizationDelay = new System.Windows.Forms.NumericUpDown();
            this.manualTestDS = new System.Data.DataSet();
            this.camera_types = new System.Data.DataTable();
            this.camera_type_id = new System.Data.DataColumn();
            this.camera_type_name = new System.Data.DataColumn();
            this.camera_type_internal_diameter = new System.Data.DataColumn();
            this.dataColumn1 = new System.Data.DataColumn();
            this.isolation_materials = new System.Data.DataTable();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.bringing_types = new System.Data.DataTable();
            this.bringing_type_id = new System.Data.DataColumn();
            this.bringing_type_name = new System.Data.DataColumn();
            this.materialTypes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cameraLbl = new System.Windows.Forms.Label();
            this.cameraTypesCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bringingTypeCB = new System.Windows.Forms.ComboBox();
            this.materialLengthLbl = new System.Windows.Forms.Label();
            this.materialLength = new System.Windows.Forms.NumericUpDown();
            this.diametersLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cycleTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagingTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dischargeDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.polarizationDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.manualTestDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camera_types)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.isolation_materials)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bringing_types)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialLength)).BeginInit();
            this.SuspendLayout();
            // 
            // voltageComboBox
            // 
            this.voltageComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.voltageComboBox.FormattingEnabled = true;
            this.voltageComboBox.Items.AddRange(new object[] {
            "10",
            "100",
            "500",
            "1000"});
            this.voltageComboBox.Location = new System.Drawing.Point(18, 42);
            this.voltageComboBox.Name = "voltageComboBox";
            this.voltageComboBox.Size = new System.Drawing.Size(94, 21);
            this.voltageComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Напряжение, В";
            // 
            // startMeasureBut
            // 
            this.startMeasureBut.Location = new System.Drawing.Point(12, 496);
            this.startMeasureBut.Name = "startMeasureBut";
            this.startMeasureBut.Size = new System.Drawing.Size(145, 31);
            this.startMeasureBut.TabIndex = 2;
            this.startMeasureBut.Text = "Запуск измерения";
            this.startMeasureBut.UseVisualStyleBackColor = true;
            this.startMeasureBut.Click += new System.EventHandler(this.startMeasureBut_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.measureTimesLbl);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cycleTimes);
            this.groupBox1.Controls.Add(this.averagingTimes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dischargeDelay);
            this.groupBox1.Controls.Add(this.isCyclicMeasure);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.polarizationDelay);
            this.groupBox1.Controls.Add(this.voltageComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(760, 95);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Настройки измерителя";
            // 
            // measureTimesLbl
            // 
            this.measureTimesLbl.AutoSize = true;
            this.measureTimesLbl.Location = new System.Drawing.Point(591, 26);
            this.measureTimesLbl.Name = "measureTimesLbl";
            this.measureTimesLbl.Size = new System.Drawing.Size(125, 13);
            this.measureTimesLbl.TabIndex = 10;
            this.measureTimesLbl.Text = "Количество измерений";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(332, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Усреднение";
            // 
            // cycleTimes
            // 
            this.cycleTimes.Location = new System.Drawing.Point(594, 41);
            this.cycleTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cycleTimes.Name = "cycleTimes";
            this.cycleTimes.Size = new System.Drawing.Size(140, 20);
            this.cycleTimes.TabIndex = 9;
            this.cycleTimes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // averagingTimes
            // 
            this.averagingTimes.Location = new System.Drawing.Point(335, 42);
            this.averagingTimes.Name = "averagingTimes";
            this.averagingTimes.Size = new System.Drawing.Size(94, 20);
            this.averagingTimes.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(226, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Время разряда, с";
            // 
            // dischargeDelay
            // 
            this.dischargeDelay.Location = new System.Drawing.Point(229, 42);
            this.dischargeDelay.Name = "dischargeDelay";
            this.dischargeDelay.Size = new System.Drawing.Size(94, 20);
            this.dischargeDelay.TabIndex = 4;
            // 
            // isCyclicMeasure
            // 
            this.isCyclicMeasure.AutoSize = true;
            this.isCyclicMeasure.Location = new System.Drawing.Point(445, 43);
            this.isCyclicMeasure.Name = "isCyclicMeasure";
            this.isCyclicMeasure.Size = new System.Drawing.Size(140, 17);
            this.isCyclicMeasure.TabIndex = 8;
            this.isCyclicMeasure.Text = "Цикличное измерение";
            this.isCyclicMeasure.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(121, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выдержка, мин";
            // 
            // polarizationDelay
            // 
            this.polarizationDelay.Location = new System.Drawing.Point(124, 42);
            this.polarizationDelay.Name = "polarizationDelay";
            this.polarizationDelay.Size = new System.Drawing.Size(94, 20);
            this.polarizationDelay.TabIndex = 2;
            // 
            // manualTestDS
            // 
            this.manualTestDS.DataSetName = "NewDataSet";
            this.manualTestDS.Tables.AddRange(new System.Data.DataTable[] {
            this.camera_types,
            this.isolation_materials,
            this.bringing_types});
            // 
            // camera_types
            // 
            this.camera_types.Columns.AddRange(new System.Data.DataColumn[] {
            this.camera_type_id,
            this.camera_type_name,
            this.camera_type_internal_diameter,
            this.dataColumn1});
            this.camera_types.TableName = "camera_types";
            // 
            // camera_type_id
            // 
            this.camera_type_id.ColumnName = "id";
            // 
            // camera_type_name
            // 
            this.camera_type_name.ColumnName = "name";
            // 
            // camera_type_internal_diameter
            // 
            this.camera_type_internal_diameter.ColumnName = "internal_diameter";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "external_diameter";
            // 
            // isolation_materials
            // 
            this.isolation_materials.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn2,
            this.dataColumn3});
            this.isolation_materials.TableName = "isolation_materials";
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "id";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "name";
            // 
            // bringing_types
            // 
            this.bringing_types.Columns.AddRange(new System.Data.DataColumn[] {
            this.bringing_type_id,
            this.bringing_type_name});
            this.bringing_types.TableName = "bringing_types";
            // 
            // bringing_type_id
            // 
            this.bringing_type_id.ColumnName = "id";
            // 
            // bringing_type_name
            // 
            this.bringing_type_name.ColumnName = "name";
            // 
            // materialTypes
            // 
            this.materialTypes.DataSource = this.manualTestDS;
            this.materialTypes.DisplayMember = "isolation_materials.name";
            this.materialTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.materialTypes.FormattingEnabled = true;
            this.materialTypes.Location = new System.Drawing.Point(30, 139);
            this.materialTypes.Name = "materialTypes";
            this.materialTypes.Size = new System.Drawing.Size(148, 21);
            this.materialTypes.TabIndex = 4;
            this.materialTypes.ValueMember = "isolation_materials.id";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Материал";
            // 
            // cameraLbl
            // 
            this.cameraLbl.AutoSize = true;
            this.cameraLbl.Location = new System.Drawing.Point(27, 225);
            this.cameraLbl.Name = "cameraLbl";
            this.cameraLbl.Size = new System.Drawing.Size(151, 13);
            this.cameraLbl.TabIndex = 6;
            this.cameraLbl.Text = "Тип измерительной камеры";
            // 
            // cameraTypesCB
            // 
            this.cameraTypesCB.DataSource = this.manualTestDS;
            this.cameraTypesCB.DisplayMember = "camera_types.name";
            this.cameraTypesCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cameraTypesCB.FormattingEnabled = true;
            this.cameraTypesCB.Location = new System.Drawing.Point(30, 241);
            this.cameraTypesCB.Name = "cameraTypesCB";
            this.cameraTypesCB.Size = new System.Drawing.Size(148, 21);
            this.cameraTypesCB.TabIndex = 7;
            this.cameraTypesCB.ValueMember = "camera_types.id";
            this.cameraTypesCB.SelectedIndexChanged += new System.EventHandler(this.cameraTypesCB_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 172);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Тип приведения";
            // 
            // bringingTypeCB
            // 
            this.bringingTypeCB.DataSource = this.manualTestDS;
            this.bringingTypeCB.DisplayMember = "bringing_types.name";
            this.bringingTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bringingTypeCB.FormattingEnabled = true;
            this.bringingTypeCB.Location = new System.Drawing.Point(30, 188);
            this.bringingTypeCB.Name = "bringingTypeCB";
            this.bringingTypeCB.Size = new System.Drawing.Size(148, 21);
            this.bringingTypeCB.TabIndex = 9;
            this.bringingTypeCB.ValueMember = "bringing_types.id";
            this.bringingTypeCB.SelectedIndexChanged += new System.EventHandler(this.bringingTypeCB_SelectedIndexChanged);
            // 
            // materialLengthLbl
            // 
            this.materialLengthLbl.AutoSize = true;
            this.materialLengthLbl.Location = new System.Drawing.Point(27, 225);
            this.materialLengthLbl.Name = "materialLengthLbl";
            this.materialLengthLbl.Size = new System.Drawing.Size(117, 13);
            this.materialLengthLbl.TabIndex = 10;
            this.materialLengthLbl.Text = "Длина приведения, м";
            // 
            // materialLength
            // 
            this.materialLength.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.materialLength.Location = new System.Drawing.Point(30, 242);
            this.materialLength.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.materialLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.materialLength.Name = "materialLength";
            this.materialLength.Size = new System.Drawing.Size(148, 20);
            this.materialLength.TabIndex = 11;
            this.materialLength.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // diametersLbl
            // 
            this.diametersLbl.AutoSize = true;
            this.diametersLbl.Location = new System.Drawing.Point(27, 265);
            this.diametersLbl.Name = "diametersLbl";
            this.diametersLbl.Size = new System.Drawing.Size(35, 13);
            this.diametersLbl.TabIndex = 12;
            this.diametersLbl.Text = "label6";
            // 
            // manualMeasureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 539);
            this.Controls.Add(this.diametersLbl);
            this.Controls.Add(this.materialLength);
            this.Controls.Add(this.materialLengthLbl);
            this.Controls.Add(this.bringingTypeCB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cameraTypesCB);
            this.Controls.Add(this.cameraLbl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.materialTypes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.startMeasureBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "manualMeasureForm";
            this.Text = "Ручные измерения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.manualMeasureForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cycleTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.averagingTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dischargeDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.polarizationDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.manualTestDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camera_types)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.isolation_materials)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bringing_types)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox voltageComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startMeasureBut;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label measureTimesLbl;
        private System.Windows.Forms.NumericUpDown cycleTimes;
        private System.Windows.Forms.CheckBox isCyclicMeasure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown averagingTimes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown dischargeDelay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown polarizationDelay;
        private System.Data.DataSet manualTestDS;
        private System.Data.DataTable camera_types;
        private System.Data.DataColumn camera_type_id;
        private System.Data.DataColumn camera_type_name;
        private System.Data.DataColumn camera_type_internal_diameter;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataTable isolation_materials;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Windows.Forms.ComboBox materialTypes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label cameraLbl;
        private System.Windows.Forms.ComboBox cameraTypesCB;
        private System.Data.DataTable bringing_types;
        private System.Data.DataColumn bringing_type_id;
        private System.Data.DataColumn bringing_type_name;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox bringingTypeCB;
        private System.Windows.Forms.Label materialLengthLbl;
        private System.Windows.Forms.NumericUpDown materialLength;
        private System.Windows.Forms.Label diametersLbl;
    }
}