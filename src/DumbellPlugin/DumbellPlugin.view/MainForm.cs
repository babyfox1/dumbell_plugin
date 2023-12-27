namespace DumbellPlugin.View
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using DumbellPlugin.Model;

    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : Form
    {

        /// <summary>
        /// ��������� ������ ����������.
        /// </summary>
        private readonly Parameters _parameters = new Parameters();

        /// <summary>
        /// ��������� ������ ���������.
        /// </summary>
        private readonly Builder _builder = new Builder();

        /// <summary>
        /// �������, ���������� �������� ���������� ����� ��� ������� ����
        /// ���������.
        /// </summary>
        private readonly Dictionary<ParameterType, Dictionary<string, Control>>
            _parameterFormElements = new Dictionary<ParameterType, Dictionary<string, Control>>();

        /// <summary>
        /// ���� �� ��������� ��� ��������� �����.
        /// </summary>
        private readonly Color _defaultColor = Color.White;

        /// <summary>
        /// ���� ��� ����������� ������ �����.
        /// </summary>
        private readonly Color _errorColor =
            Color.FromArgb(255, 192, 192);

        /// <summary>
        /// ������ ������������ textBox.
        /// </summary>
        private readonly string _textBox = "textBox";

        /// <summary>
        /// ������ ������������ label.
        /// </summary>
        private readonly string _label = "label";

        public MainForm()
        {
            InitializeComponent();

        }

        /// <summary>
        /// ���������� ������� �������� �����.
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeParameterFormElements();
            SetTextFormElements();
        }

        /// <summary>
        /// �������������� �������� ���������� ����� ��� ������� ����
        /// ���������.
        /// </summary>
        private void InitializeParameterFormElements()
        {
            _parameterFormElements.Add(
                ParameterType.LengthHandle,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_LengthHandle },
                    { _label, label_LengthHandle },
                });
            _parameterFormElements.Add(
                ParameterType.DiameterHandle,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_DiameterHandle },
                    { _label, label_DiameterHandle },
                });
            _parameterFormElements.Add(
                ParameterType.WidthFasten,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_WidthFasten },
                    { _label, label_WidthFasten },
                });
            _parameterFormElements.Add(
                ParameterType.DiameterFasten,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_DiameterFasten },
                    { _label, label_DiameterFasten },
                });
            _parameterFormElements.Add(
                ParameterType.AmountDisk,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_AmountDisk },
                    { _label, label_AmountDisk },
                });
            _parameterFormElements.Add(
                ParameterType.OuterDiameterDisk,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_OuterDiameterDisk },
                    { _label, label_OuterDiameterDisk },
                });
            _parameterFormElements.Add(
                ParameterType.InnerDiameterDisk,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_InnerDiameterDisk },
                    { _label, label_InnerDiameterDisk },
                });
            _parameterFormElements.Add(
                ParameterType.WidthDisk,
                new Dictionary<string, Control>
                {
                    { _textBox, textBox_WidthDisk },
                    { _label, label10},
                });
            SetTextFormElements();
        }

        /// <summary>
        /// ���������� ������� ��������� ������ � ��������� ����.
        /// </summary>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                var textBoxName = textBox.Name;
                var parameterType = ParameterType.Unknown;

                var parameterTypeStr =
                    textBoxName.Split('_')[1];

                foreach (var item in _parameterFormElements.Keys)
                {
                    if (item.ToString() == parameterTypeStr)
                    {
                        parameterType = item;
                        break;
                    }
                }

                try
                {
                    _parameters.AssertParameter(
                        parameterType,
                        _parameters.ParametersDict[parameterType],
                        Convert.ToDouble(textBox.Text));
                    SetTextFormElements();
                    _parameterFormElements[parameterType][_textBox].
                        BackColor = _defaultColor;
                    buttonBuild.Enabled = true;
                }
                catch (Exception ex)
                {
                    var parameter =
                        _parameters.ParametersDict[parameterType];
                    var minValue = parameter.MinValue;
                    var maxValue = parameter.MaxValue;
                    var message =
                        ex.Message + "\n������� ����� �� "
                                   + $"{minValue} �� {maxValue}";
                    _parameterFormElements[parameterType][_label].
                        Text = message;
                    _parameterFormElements[parameterType][_textBox].
                        BackColor = _errorColor;
                    buttonBuild.Enabled = false;
                }
            }
        }

        /// <summary>
        /// ������������� ����� � �������� ��������� ���������� ����� ��
        /// ������ ������� ����������.
        /// </summary>
        private void SetTextFormElements()
        {
            foreach (var item
                in _parameters.ParametersDict)
            {
                var _key = item.Key;
                var _value = item.Value;

                _parameterFormElements[_key][_textBox].Text =
                    _value.CurrentValue.ToString();
                _parameterFormElements[_key][_label].Text =
                    $"�� {_value.MinValue} �� {_value.MaxValue}";
            }
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            _builder.BuildDetail(_parameters);
        }

    }
}