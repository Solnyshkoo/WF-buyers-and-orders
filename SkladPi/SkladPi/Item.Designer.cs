
namespace SkladPi
{
    partial class Item
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.mainProperties = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMinAmount = new System.Windows.Forms.TextBox();
            this.tbAmount = new System.Windows.Forms.TextBox();
            this.tbPrice = new System.Windows.Forms.TextBox();
            this.tbArticle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFullPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tBItemName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.description = new System.Windows.Forms.TabPage();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.photo = new System.Windows.Forms.TabPage();
            this.Save = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.mainProperties.SuspendLayout();
            this.description.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.mainProperties);
            this.tabControl1.Controls.Add(this.description);
            this.tabControl1.Controls.Add(this.photo);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1073, 595);
            this.tabControl1.TabIndex = 0;
            // 
            // mainProperties
            // 
            this.mainProperties.Controls.Add(this.label6);
            this.mainProperties.Controls.Add(this.label5);
            this.mainProperties.Controls.Add(this.label4);
            this.mainProperties.Controls.Add(this.tbMinAmount);
            this.mainProperties.Controls.Add(this.tbAmount);
            this.mainProperties.Controls.Add(this.tbPrice);
            this.mainProperties.Controls.Add(this.tbArticle);
            this.mainProperties.Controls.Add(this.label3);
            this.mainProperties.Controls.Add(this.tbFullPath);
            this.mainProperties.Controls.Add(this.label2);
            this.mainProperties.Controls.Add(this.tBItemName);
            this.mainProperties.Controls.Add(this.label1);
            this.mainProperties.Location = new System.Drawing.Point(8, 46);
            this.mainProperties.Name = "mainProperties";
            this.mainProperties.Padding = new System.Windows.Forms.Padding(3);
            this.mainProperties.Size = new System.Drawing.Size(1057, 541);
            this.mainProperties.TabIndex = 0;
            this.mainProperties.Text = "Основные свойства";
            this.mainProperties.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 408);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(386, 32);
            this.label6.TabIndex = 11;
            this.label6.Text = "Минимальный остаток на складе:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(476, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 32);
            this.label5.TabIndex = 10;
            this.label5.Text = "Количество:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 32);
            this.label4.TabIndex = 9;
            this.label4.Text = "Цена:";
            // 
            // tbMinAmount
            // 
            this.tbMinAmount.Location = new System.Drawing.Point(462, 408);
            this.tbMinAmount.Name = "tbMinAmount";
            this.tbMinAmount.Size = new System.Drawing.Size(200, 39);
            this.tbMinAmount.TabIndex = 8;
            // 
            // tbAmount
            // 
            this.tbAmount.Location = new System.Drawing.Point(653, 300);
            this.tbAmount.Name = "tbAmount";
            this.tbAmount.Size = new System.Drawing.Size(169, 39);
            this.tbAmount.TabIndex = 7;
            // 
            // tbPrice
            // 
            this.tbPrice.Location = new System.Drawing.Point(151, 300);
            this.tbPrice.Name = "tbPrice";
            this.tbPrice.Size = new System.Drawing.Size(151, 39);
            this.tbPrice.TabIndex = 6;
            // 
            // tbArticle
            // 
            this.tbArticle.Location = new System.Drawing.Point(174, 174);
            this.tbArticle.Name = "tbArticle";
            this.tbArticle.Size = new System.Drawing.Size(280, 39);
            this.tbArticle.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Артикул:";
            // 
            // tbFullPath
            // 
            this.tbFullPath.Location = new System.Drawing.Point(216, 109);
            this.tbFullPath.Name = "tbFullPath";
            this.tbFullPath.ReadOnly = true;
            this.tbFullPath.Size = new System.Drawing.Size(584, 39);
            this.tbFullPath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Полный путь:";
            // 
            // tBItemName
            // 
            this.tBItemName.Location = new System.Drawing.Point(195, 36);
            this.tBItemName.Name = "tBItemName";
            this.tBItemName.Size = new System.Drawing.Size(249, 39);
            this.tBItemName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название: ";
            // 
            // description
            // 
            this.description.Controls.Add(this.rtbDescription);
            this.description.Location = new System.Drawing.Point(8, 46);
            this.description.Name = "description";
            this.description.Padding = new System.Windows.Forms.Padding(3);
            this.description.Size = new System.Drawing.Size(1057, 541);
            this.description.TabIndex = 1;
            this.description.Text = "Описание";
            this.description.UseVisualStyleBackColor = true;
            // 
            // rtbDescription
            // 
            this.rtbDescription.Location = new System.Drawing.Point(3, 3);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(1051, 535);
            this.rtbDescription.TabIndex = 0;
            this.rtbDescription.Text = "";
            // 
            // photo
            // 
            this.photo.Location = new System.Drawing.Point(8, 46);
            this.photo.Name = "photo";
            this.photo.Padding = new System.Windows.Forms.Padding(3);
            this.photo.Size = new System.Drawing.Size(1057, 541);
            this.photo.TabIndex = 2;
            this.photo.Text = "Фото";
            this.photo.UseVisualStyleBackColor = true;
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.Location = new System.Drawing.Point(496, 628);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(150, 46);
            this.Save.TabIndex = 1;
            this.Save.Text = "Сохранить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.Location = new System.Drawing.Point(692, 628);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(150, 46);
            this.bDelete.TabIndex = 2;
            this.bDelete.Text = "Удалить";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(888, 628);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(150, 46);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 702);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.tabControl1);
            this.Name = "Item";
            this.Text = "Карточка товара";
            this.Load += new System.EventHandler(this.Item_Load);
            this.tabControl1.ResumeLayout(false);
            this.mainProperties.ResumeLayout(false);
            this.mainProperties.PerformLayout();
            this.description.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage mainProperties;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage description;
        private System.Windows.Forms.TabPage photo;
        private System.Windows.Forms.TextBox tBItemName;
        private System.Windows.Forms.TextBox tbFullPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMinAmount;
        private System.Windows.Forms.TextBox tbAmount;
        private System.Windows.Forms.TextBox tbPrice;
        private System.Windows.Forms.TextBox tbArticle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bCancel;
    }
}