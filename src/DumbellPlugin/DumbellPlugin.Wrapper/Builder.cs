// <copyright file="Builder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Model
{
    // TODO: подключить dll из папки Libs - Исправил
    using Kompas6API5;
    using Kompas6Constants3D;

    /// <summary>
    /// Класс для построения деталей в KOMPAS-3D.
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Экземпляр класса обертки.
        /// </summary>
        private readonly Wrapper wrapper = new Wrapper();

        /// <summary>
        /// Парраметр.
        /// </summary>
        private Parameters parameters = new Parameters();

        // TODO: XML

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// Конструктор.
        /// </summary>
        public Builder()
        {
        }

        /// <summary>
        /// Строит деталь на основе заданных параметров.
        /// </summary>
        /// <param name="parameters">Параметры для построения детали.</param>
        public void BuildDetail(Parameters parameters)
        {
            this.parameters = parameters;
            this.wrapper.ConnectToKompas();
            this.wrapper.CreateDocument3D();

            this.BuildHandle();
            this.BuildDisks();
            this.BuildFastening();
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {
            double lengthHandle = this.parameters.GetParameter(ParameterType.LengthHandle);
            double diameterHandle = this.parameters.GetParameter(ParameterType.DiameterHandle);

            // Создаем эскиз влево
            // TODO: длинная строка
            var sketchHandle = this.wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, -1 * lengthHandle / 2);

            // TODO: naming
            var document2d = (ksDocument2D)sketchHandle.BeginEdit();

            // Создаем круг радиусом 50(радиус рукоятки)
            document2d.ksCircle(0, 0, diameterHandle, 1);

            sketchHandle.EndEdit();

            // Теперь передаем этот эскиз в метод CreateExtrusion
            // TODO: нужно?
            var extrusionDefL = this.wrapper.CreateExtrusion(sketchHandle, lengthHandle, false);
        }

        /// <summary>
        /// Строит диски детали.
        /// </summary>
        private void BuildDisks()
        {
            double amountDisk = this.parameters.GetParameter(ParameterType.AmountDisk);
            double outerDiameterDisk = this.parameters.GetParameter(ParameterType.OuterDiameterDisk);

            // TODO: нужно? - исправил
            double lengthHandle = this.parameters.GetParameter(ParameterType.LengthHandle);
            double widthDisk = this.parameters.GetParameter(ParameterType.WidthDisk);

            double diameterHandle = this.parameters.GetParameter(ParameterType.DiameterHandle);

            // TODO: магическое число - исправил
            // Создаем внутренний диаметр диска чуть больше диаметра рукоятки
            double hANDLE_INNER_DISK_PROP = 1.05;
            double changeInnerDiameterDisk = diameterHandle * hANDLE_INNER_DISK_PROP;

            // Создаем расстояние от центра до начала выдавливания дисков
            double eXTRUSION_LENGTH_DISK = 85.0;

            // Физическое расстояние между дисками
            double lENGHT_BTW_DISKS = 2.0;

            // Расстояние между началом выдавливания дисков
            // TODO: магическое число - исправил
            double offset = widthDisk + lENGHT_BTW_DISKS;

            for (int i = 0; i < amountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                // TODO: магическое число
                var sketch = this.wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, eXTRUSION_LENGTH_DISK + (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, outerDiameterDisk, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                this.wrapper.CreateExtrusion(sketch, widthDisk, false);
            }

            for (int i = 0; i < amountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                // TODO: магическое число
                var sketch = this.wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, (-1 * eXTRUSION_LENGTH_DISK) - widthDisk - (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // TODO: комментарии над строкой
                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, outerDiameterDisk, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                this.wrapper.CreateExtrusion(sketch, widthDisk, false);
            }
        }

        private void BuildFastening()
        {
            // double WidthDisk = _parameters.GetParameter(ParameterType.WidthDisk);
            // TODO: RSDN
            double diameterFasten = this.parameters.GetParameter(ParameterType.DiameterFasten);
            double widthFasten = this.parameters.GetParameter(ParameterType.WidthFasten);
            double diameterHandle = this.parameters.GetParameter(ParameterType.DiameterHandle);

            // Соотношение диаметров крепления и рукоятки
            double rATIO_DIAMETER_HANDLE_FASTEN = 2.0;
            double changeDiameterFasten = diameterHandle * rATIO_DIAMETER_HANDLE_FASTEN;

            // Расстояние от центра, где будет начинать выдавливаться крепления
            double lENGTH_FASTEN_CREATE = 75.0;

            // Создаем эскиз для первого диска на расстоянии 120 от начала координат
            var sketch1 = this.wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, (-1 * lENGTH_FASTEN_CREATE) - widthFasten);
            var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
            document2d_1.ksCircle(0, 0, changeDiameterFasten, 1); // Внешний круг радиусом 200

            sketch1.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            this.wrapper.CreateExtrusion(sketch1, widthFasten, false);

            var sketch2 = this.wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 75);
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(0, 0, changeDiameterFasten, 1); // Внешний круг радиусом 200

            sketch2.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            this.wrapper.CreateExtrusion(sketch2, widthFasten, false);
        }
    }
}
