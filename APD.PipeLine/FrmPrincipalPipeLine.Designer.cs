//===============================================================================
// Microsoft patterns & practices
// Parallel Programming Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://parallelpatterns.codeplex.com/license).
//===============================================================================

namespace APD.PipeLine
{
    partial class FrmPrincipalPipeLine
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
            if (cts != null)
            {
                cts.Dispose();
                cts = null;
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
            this.pcbImg1 = new System.Windows.Forms.PictureBox();
            this.btnSair = new System.Windows.Forms.Button();
            this.txt1CarregadasTempoCrescimento = new System.Windows.Forms.TextBox();
            this.txt2EscalaAlteradaTempoCrescimento = new System.Windows.Forms.TextBox();
            this.txt3FiltroTonsCinzaTempoCrescimento = new System.Windows.Forms.TextBox();
            this.txt4VisualizadasTempoCrescimento = new System.Windows.Forms.TextBox();
            this.txtNomeArquivo = new System.Windows.Forms.TextBox();
            this.txtContadorImagens = new System.Windows.Forms.TextBox();
            this.txtTempoPorImagem = new System.Windows.Forms.TextBox();
            this.txtFila1TempoEspera = new System.Windows.Forms.TextBox();
            this.txtFila2TempoEspera = new System.Windows.Forms.TextBox();
            this.txtFila3TempoEspera = new System.Windows.Forms.TextBox();
            this.txtFila1Contagem = new System.Windows.Forms.TextBox();
            this.txtFila2Contagem = new System.Windows.Forms.TextBox();
            this.txtFila3Contagem = new System.Windows.Forms.TextBox();
            this.lblCarregadas = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblEscalaAlterada = new System.Windows.Forms.Label();
            this.lblFiltroTonsDeCinza = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblVisualizadas = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnIniciar = new System.Windows.Forms.Button();
            this.radSequencial = new System.Windows.Forms.RadioButton();
            this.radPipeline = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnParar = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gpbTempoPorFase = new System.Windows.Forms.GroupBox();
            this.lblTempoEsperaFila = new System.Windows.Forms.Label();
            this.lblImagens = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lnkGabrielVicente = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pcbImg1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gpbTempoPorFase.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcbImg1
            // 
            this.pcbImg1.Location = new System.Drawing.Point(9, 10);
            this.pcbImg1.Margin = new System.Windows.Forms.Padding(2);
            this.pcbImg1.Name = "pcbImg1";
            this.pcbImg1.Size = new System.Drawing.Size(240, 204);
            this.pcbImg1.TabIndex = 0;
            this.pcbImg1.TabStop = false;
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(465, 247);
            this.btnSair.Margin = new System.Windows.Forms.Padding(2);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(56, 32);
            this.btnSair.TabIndex = 1;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // txt1CarregadasTempoCrescimento
            // 
            this.txt1CarregadasTempoCrescimento.Location = new System.Drawing.Point(117, 28);
            this.txt1CarregadasTempoCrescimento.Margin = new System.Windows.Forms.Padding(2);
            this.txt1CarregadasTempoCrescimento.Name = "txt1CarregadasTempoCrescimento";
            this.txt1CarregadasTempoCrescimento.ReadOnly = true;
            this.txt1CarregadasTempoCrescimento.Size = new System.Drawing.Size(48, 20);
            this.txt1CarregadasTempoCrescimento.TabIndex = 2;
            // 
            // txt2EscalaAlteradaTempoCrescimento
            // 
            this.txt2EscalaAlteradaTempoCrescimento.Location = new System.Drawing.Point(117, 52);
            this.txt2EscalaAlteradaTempoCrescimento.Margin = new System.Windows.Forms.Padding(2);
            this.txt2EscalaAlteradaTempoCrescimento.Name = "txt2EscalaAlteradaTempoCrescimento";
            this.txt2EscalaAlteradaTempoCrescimento.ReadOnly = true;
            this.txt2EscalaAlteradaTempoCrescimento.Size = new System.Drawing.Size(48, 20);
            this.txt2EscalaAlteradaTempoCrescimento.TabIndex = 2;
            // 
            // txt3FiltroTonsCinzaTempoCrescimento
            // 
            this.txt3FiltroTonsCinzaTempoCrescimento.Location = new System.Drawing.Point(117, 76);
            this.txt3FiltroTonsCinzaTempoCrescimento.Margin = new System.Windows.Forms.Padding(2);
            this.txt3FiltroTonsCinzaTempoCrescimento.Name = "txt3FiltroTonsCinzaTempoCrescimento";
            this.txt3FiltroTonsCinzaTempoCrescimento.ReadOnly = true;
            this.txt3FiltroTonsCinzaTempoCrescimento.Size = new System.Drawing.Size(48, 20);
            this.txt3FiltroTonsCinzaTempoCrescimento.TabIndex = 2;
            // 
            // txt4VisualizadasTempoCrescimento
            // 
            this.txt4VisualizadasTempoCrescimento.Location = new System.Drawing.Point(117, 100);
            this.txt4VisualizadasTempoCrescimento.Margin = new System.Windows.Forms.Padding(2);
            this.txt4VisualizadasTempoCrescimento.Name = "txt4VisualizadasTempoCrescimento";
            this.txt4VisualizadasTempoCrescimento.ReadOnly = true;
            this.txt4VisualizadasTempoCrescimento.Size = new System.Drawing.Size(48, 20);
            this.txt4VisualizadasTempoCrescimento.TabIndex = 2;
            // 
            // txtNomeArquivo
            // 
            this.txtNomeArquivo.Location = new System.Drawing.Point(9, 217);
            this.txtNomeArquivo.Margin = new System.Windows.Forms.Padding(2);
            this.txtNomeArquivo.Name = "txtNomeArquivo";
            this.txtNomeArquivo.ReadOnly = true;
            this.txtNomeArquivo.Size = new System.Drawing.Size(240, 20);
            this.txtNomeArquivo.TabIndex = 3;
            // 
            // txtContadorImagens
            // 
            this.txtContadorImagens.Location = new System.Drawing.Point(262, 217);
            this.txtContadorImagens.Margin = new System.Windows.Forms.Padding(2);
            this.txtContadorImagens.Name = "txtContadorImagens";
            this.txtContadorImagens.ReadOnly = true;
            this.txtContadorImagens.Size = new System.Drawing.Size(48, 20);
            this.txtContadorImagens.TabIndex = 2;
            // 
            // txtTempoPorImagem
            // 
            this.txtTempoPorImagem.Location = new System.Drawing.Point(473, 217);
            this.txtTempoPorImagem.Margin = new System.Windows.Forms.Padding(2);
            this.txtTempoPorImagem.Name = "txtTempoPorImagem";
            this.txtTempoPorImagem.ReadOnly = true;
            this.txtTempoPorImagem.Size = new System.Drawing.Size(48, 20);
            this.txtTempoPorImagem.TabIndex = 2;
            // 
            // txtFila1TempoEspera
            // 
            this.txtFila1TempoEspera.Location = new System.Drawing.Point(541, 58);
            this.txtFila1TempoEspera.Margin = new System.Windows.Forms.Padding(2);
            this.txtFila1TempoEspera.Name = "txtFila1TempoEspera";
            this.txtFila1TempoEspera.ReadOnly = true;
            this.txtFila1TempoEspera.Size = new System.Drawing.Size(48, 20);
            this.txtFila1TempoEspera.TabIndex = 2;
            this.txtFila1TempoEspera.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFila2TempoEspera
            // 
            this.txtFila2TempoEspera.Location = new System.Drawing.Point(541, 81);
            this.txtFila2TempoEspera.Margin = new System.Windows.Forms.Padding(2);
            this.txtFila2TempoEspera.Name = "txtFila2TempoEspera";
            this.txtFila2TempoEspera.ReadOnly = true;
            this.txtFila2TempoEspera.Size = new System.Drawing.Size(48, 20);
            this.txtFila2TempoEspera.TabIndex = 2;
            this.txtFila2TempoEspera.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFila3TempoEspera
            // 
            this.txtFila3TempoEspera.Location = new System.Drawing.Point(541, 104);
            this.txtFila3TempoEspera.Margin = new System.Windows.Forms.Padding(2);
            this.txtFila3TempoEspera.Name = "txtFila3TempoEspera";
            this.txtFila3TempoEspera.ReadOnly = true;
            this.txtFila3TempoEspera.Size = new System.Drawing.Size(48, 20);
            this.txtFila3TempoEspera.TabIndex = 2;
            this.txtFila3TempoEspera.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFila1Contagem
            // 
            this.txtFila1Contagem.Location = new System.Drawing.Point(646, 57);
            this.txtFila1Contagem.Margin = new System.Windows.Forms.Padding(2);
            this.txtFila1Contagem.Name = "txtFila1Contagem";
            this.txtFila1Contagem.ReadOnly = true;
            this.txtFila1Contagem.Size = new System.Drawing.Size(48, 20);
            this.txtFila1Contagem.TabIndex = 2;
            this.txtFila1Contagem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFila2Contagem
            // 
            this.txtFila2Contagem.Location = new System.Drawing.Point(646, 80);
            this.txtFila2Contagem.Margin = new System.Windows.Forms.Padding(2);
            this.txtFila2Contagem.Name = "txtFila2Contagem";
            this.txtFila2Contagem.ReadOnly = true;
            this.txtFila2Contagem.Size = new System.Drawing.Size(48, 20);
            this.txtFila2Contagem.TabIndex = 2;
            this.txtFila2Contagem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtFila3Contagem
            // 
            this.txtFila3Contagem.Location = new System.Drawing.Point(646, 103);
            this.txtFila3Contagem.Margin = new System.Windows.Forms.Padding(2);
            this.txtFila3Contagem.Name = "txtFila3Contagem";
            this.txtFila3Contagem.ReadOnly = true;
            this.txtFila3Contagem.Size = new System.Drawing.Size(48, 20);
            this.txtFila3Contagem.TabIndex = 2;
            this.txtFila3Contagem.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCarregadas
            // 
            this.lblCarregadas.AutoSize = true;
            this.lblCarregadas.Location = new System.Drawing.Point(47, 31);
            this.lblCarregadas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCarregadas.Name = "lblCarregadas";
            this.lblCarregadas.Size = new System.Drawing.Size(64, 13);
            this.lblCarregadas.TabIndex = 4;
            this.lblCarregadas.Text = "Carregadas:";
            this.lblCarregadas.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(592, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fila 2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(592, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Fila 3";
            // 
            // lblEscalaAlterada
            // 
            this.lblEscalaAlterada.AutoSize = true;
            this.lblEscalaAlterada.Location = new System.Drawing.Point(27, 55);
            this.lblEscalaAlterada.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblEscalaAlterada.Name = "lblEscalaAlterada";
            this.lblEscalaAlterada.Size = new System.Drawing.Size(84, 13);
            this.lblEscalaAlterada.TabIndex = 4;
            this.lblEscalaAlterada.Text = "Escala Alterada:";
            this.lblEscalaAlterada.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblFiltroTonsDeCinza
            // 
            this.lblFiltroTonsDeCinza.AutoSize = true;
            this.lblFiltroTonsDeCinza.Location = new System.Drawing.Point(10, 79);
            this.lblFiltroTonsDeCinza.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFiltroTonsDeCinza.Name = "lblFiltroTonsDeCinza";
            this.lblFiltroTonsDeCinza.Size = new System.Drawing.Size(103, 13);
            this.lblFiltroTonsDeCinza.TabIndex = 4;
            this.lblFiltroTonsDeCinza.Text = "Filtro Tons de Cinza:";
            this.lblFiltroTonsDeCinza.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(592, 61);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Fila 1";
            // 
            // lblVisualizadas
            // 
            this.lblVisualizadas.AutoSize = true;
            this.lblVisualizadas.Location = new System.Drawing.Point(45, 103);
            this.lblVisualizadas.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblVisualizadas.Name = "lblVisualizadas";
            this.lblVisualizadas.Size = new System.Drawing.Size(68, 13);
            this.lblVisualizadas.TabIndex = 4;
            this.lblVisualizadas.Text = "Visualizadas:";
            this.lblVisualizadas.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(399, 201);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(122, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Tempo por imagem (ms):";
            this.label11.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnIniciar
            // 
            this.btnIniciar.Location = new System.Drawing.Point(342, 247);
            this.btnIniciar.Margin = new System.Windows.Forms.Padding(2);
            this.btnIniciar.Name = "btnIniciar";
            this.btnIniciar.Size = new System.Drawing.Size(56, 32);
            this.btnIniciar.TabIndex = 6;
            this.btnIniciar.Text = "Iniciar";
            this.btnIniciar.UseVisualStyleBackColor = true;
            this.btnIniciar.Click += new System.EventHandler(this.btnIniciar_Click);
            // 
            // radSequencial
            // 
            this.radSequencial.AutoSize = true;
            this.radSequencial.Location = new System.Drawing.Point(20, 14);
            this.radSequencial.Margin = new System.Windows.Forms.Padding(2);
            this.radSequencial.Name = "radSequencial";
            this.radSequencial.Size = new System.Drawing.Size(76, 17);
            this.radSequencial.TabIndex = 7;
            this.radSequencial.Text = "Sequência";
            this.radSequencial.UseVisualStyleBackColor = true;
            this.radSequencial.CheckedChanged += new System.EventHandler(this.radSequencia_CheckedChanged);
            // 
            // radPipeline
            // 
            this.radPipeline.AutoSize = true;
            this.radPipeline.Location = new System.Drawing.Point(143, 14);
            this.radPipeline.Margin = new System.Windows.Forms.Padding(2);
            this.radPipeline.Name = "radPipeline";
            this.radPipeline.Size = new System.Drawing.Size(66, 17);
            this.radPipeline.TabIndex = 7;
            this.radPipeline.Text = "PipeLine";
            this.radPipeline.UseVisualStyleBackColor = true;
            this.radPipeline.CheckedChanged += new System.EventHandler(this.radPipeline_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radPipeline);
            this.groupBox1.Controls.Add(this.radSequencial);
            this.groupBox1.Location = new System.Drawing.Point(9, 241);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(246, 38);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // btnParar
            // 
            this.btnParar.Location = new System.Drawing.Point(402, 247);
            this.btnParar.Margin = new System.Windows.Forms.Padding(2);
            this.btnParar.Name = "btnParar";
            this.btnParar.Size = new System.Drawing.Size(59, 32);
            this.btnParar.TabIndex = 9;
            this.btnParar.Text = "Parar";
            this.btnParar.UseVisualStyleBackColor = true;
            this.btnParar.Click += new System.EventHandler(this.btnParar_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(638, 26);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Tamanho";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(664, 39);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Fila:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // gpbTempoPorFase
            // 
            this.gpbTempoPorFase.Controls.Add(this.lblVisualizadas);
            this.gpbTempoPorFase.Controls.Add(this.lblFiltroTonsDeCinza);
            this.gpbTempoPorFase.Controls.Add(this.lblEscalaAlterada);
            this.gpbTempoPorFase.Controls.Add(this.lblCarregadas);
            this.gpbTempoPorFase.Controls.Add(this.txt4VisualizadasTempoCrescimento);
            this.gpbTempoPorFase.Controls.Add(this.txt3FiltroTonsCinzaTempoCrescimento);
            this.gpbTempoPorFase.Controls.Add(this.txt2EscalaAlteradaTempoCrescimento);
            this.gpbTempoPorFase.Controls.Add(this.txt1CarregadasTempoCrescimento);
            this.gpbTempoPorFase.Location = new System.Drawing.Point(253, 11);
            this.gpbTempoPorFase.Margin = new System.Windows.Forms.Padding(2);
            this.gpbTempoPorFase.Name = "gpbTempoPorFase";
            this.gpbTempoPorFase.Padding = new System.Windows.Forms.Padding(2);
            this.gpbTempoPorFase.Size = new System.Drawing.Size(227, 151);
            this.gpbTempoPorFase.TabIndex = 11;
            this.gpbTempoPorFase.TabStop = false;
            this.gpbTempoPorFase.Text = "Tempo Por fase";
            // 
            // lblTempoEsperaFila
            // 
            this.lblTempoEsperaFila.AutoSize = true;
            this.lblTempoEsperaFila.Location = new System.Drawing.Point(512, 39);
            this.lblTempoEsperaFila.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTempoEsperaFila.Name = "lblTempoEsperaFila";
            this.lblTempoEsperaFila.Size = new System.Drawing.Size(77, 13);
            this.lblTempoEsperaFila.TabIndex = 10;
            this.lblTempoEsperaFila.Text = "Espera da Fila:";
            this.lblTempoEsperaFila.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblImagens
            // 
            this.lblImagens.AutoSize = true;
            this.lblImagens.Location = new System.Drawing.Point(259, 201);
            this.lblImagens.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImagens.Name = "lblImagens";
            this.lblImagens.Size = new System.Drawing.Size(47, 13);
            this.lblImagens.TabIndex = 12;
            this.lblImagens.Text = "Imagens";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(534, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Tempo de";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lnkGabrielVicente
            // 
            this.lnkGabrielVicente.AutoSize = true;
            this.lnkGabrielVicente.LinkColor = System.Drawing.Color.SeaGreen;
            this.lnkGabrielVicente.Location = new System.Drawing.Point(613, 440);
            this.lnkGabrielVicente.Name = "lnkGabrielVicente";
            this.lnkGabrielVicente.Size = new System.Drawing.Size(175, 13);
            this.lnkGabrielVicente.TabIndex = 19;
            this.lnkGabrielVicente.TabStop = true;
            this.lnkGabrielVicente.Text = "Gabriel Vicente <gabrielvicente.ch>";
            this.lnkGabrielVicente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGabrielVicente_LinkClicked);
            // 
            // FrmPrincipalPipeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.lnkGabrielVicente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblImagens);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.gpbTempoPorFase);
            this.Controls.Add(this.lblTempoEsperaFila);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnParar);
            this.Controls.Add(this.txtTempoPorImagem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnIniciar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNomeArquivo);
            this.Controls.Add(this.txtContadorImagens);
            this.Controls.Add(this.txtFila3TempoEspera);
            this.Controls.Add(this.txtFila2TempoEspera);
            this.Controls.Add(this.txtFila1TempoEspera);
            this.Controls.Add(this.txtFila3Contagem);
            this.Controls.Add(this.txtFila2Contagem);
            this.Controls.Add(this.txtFila1Contagem);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.pcbImg1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmPrincipalPipeLine";
            this.Text = "APD | PipeLine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPrincipalPipeLine_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPrincipalPipeLine_FormClosed);
            this.Load += new System.EventHandler(this.FrmPrincipalPipeLine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbImg1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gpbTempoPorFase.ResumeLayout(false);
            this.gpbTempoPorFase.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbImg1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.TextBox txt1CarregadasTempoCrescimento;
        private System.Windows.Forms.TextBox txt2EscalaAlteradaTempoCrescimento;
        private System.Windows.Forms.TextBox txt3FiltroTonsCinzaTempoCrescimento;
        private System.Windows.Forms.TextBox txt4VisualizadasTempoCrescimento;
        private System.Windows.Forms.TextBox txtNomeArquivo;
        private System.Windows.Forms.TextBox txtContadorImagens;
        private System.Windows.Forms.TextBox txtTempoPorImagem;
        private System.Windows.Forms.TextBox txtFila1TempoEspera;
        private System.Windows.Forms.TextBox txtFila2TempoEspera;
        private System.Windows.Forms.TextBox txtFila3TempoEspera;
        private System.Windows.Forms.TextBox txtFila1Contagem;
        private System.Windows.Forms.TextBox txtFila2Contagem;
        private System.Windows.Forms.TextBox txtFila3Contagem;
        private System.Windows.Forms.Label lblCarregadas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblEscalaAlterada;
        private System.Windows.Forms.Label lblFiltroTonsDeCinza;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblVisualizadas;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnIniciar;
        private System.Windows.Forms.RadioButton radSequencial;
        private System.Windows.Forms.RadioButton radPipeline;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnParar;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox gpbTempoPorFase;
        private System.Windows.Forms.Label lblTempoEsperaFila;
        private System.Windows.Forms.Label lblImagens;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnkGabrielVicente;
    }
}

