namespace DumbellPlugin.Model
{
    // TODO: подключить dll из папки Libs
    using System.Collections.Generic;
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
        private readonly Wrapper _wrapper = new Wrapper();

        /// <summary>
        ///
        /// </summary>
        private Parameters _parameters = new Parameters();

        /// <summary>
        /// 
        /// </summary>
        public Builder(Parameters parameters)
        {
            _parameters = parameters;
        }

        // TODO: XML
        /// <summary>
        /// 
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
            _parameters = parameters;
            _wrapper.ConnectToKompas();
            _wrapper.CreateDocument3D();

            BuildHandle();
            BuildDisks();
            BuildFastening();
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {

            double LengthHandle = _parameters.GetParameter(ParameterType.LengthHandle);
            double DiameterHandle = _parameters.GetParameter(ParameterType.DiameterHandle);
            // Создаем эскиз влево
            // TODO: длинная строка
            var sketchHandleL = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, -1 * LengthHandle / 2);

            // TODO: naming
            var document2d_L = (ksDocument2D)sketchHandleL.BeginEdit();

            // Создаем круг радиусом 50(радиус рукоятки)
            document2d_L.ksCircle(0, 0, DiameterHandle, 1);

            sketchHandleL.EndEdit();


            // Теперь передаем этот эскиз в метод CreateExtrusion
            // TODO: нужно?
            var extrusionDefL = _wrapper.CreateExtrusion(sketchHandleL, LengthHandle, false);

        }


        /// <summary>
        /// Строит диски детали.
        /// </summary>
        private void BuildDisks()
        {
            double AmountDisk = _parameters.GetParameter(ParameterType.AmountDisk);
            double OuterDiameterDisk = _parameters.GetParameter(ParameterType.OuterDiameterDisk);

            // TODO: нужно?
            double LengthHandle = _parameters.GetParameter(ParameterType.LengthHandle);
            // double InnerDiameterDisk = _parameters.GetParameter(ParameterType.InnerDiameterDisk);
            double WidthDisk = _parameters.GetParameter(ParameterType.WidthDisk);

            double DiameterHandle = _parameters.GetParameter(ParameterType.DiameterHandle);

            // TODO: магическое число
            double ChangeInnerDiameterDisk = DiameterHandle * 1.05;

            // Расстояние между дисками
            // TODO: магическое число
            double offset = WidthDisk + 2;

            for (int i = 0; i < AmountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                // TODO: магическое число
                var sketch1 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 85 + i * offset);
                var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
                document2d_1.ksCircle(0, 0, OuterDiameterDisk, 1); // Внешний круг радиусом 200
                document2d_1.ksCircle(0, 0, ChangeInnerDiameterDisk, 1);  // Внутренний круг радиусом 50
                sketch1.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                _wrapper.CreateExtrusion(sketch1, WidthDisk, false);
            }

            for (int i = 0; i < AmountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                // TODO: магическое число
                var sketch1 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, -85 - WidthDisk - i * offset);
                var document2d_1 = (ksDocument2D)sketch1.BeginEdit();

                // TODO: комментарии над строкой
                document2d_1.ksCircle(0, 0, OuterDiameterDisk, 1); // Внешний круг радиусом 200
                document2d_1.ksCircle(0, 0, ChangeInnerDiameterDisk, 1);  // Внутренний круг радиусом 50
                sketch1.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                _wrapper.CreateExtrusion(sketch1, WidthDisk, false);
            }
        }




        private void BuildFastening()
        {
            // double WidthDisk = _parameters.GetParameter(ParameterType.WidthDisk);
            // TODO: RSDN
            double DiameterFasten = _parameters.GetParameter(ParameterType.DiameterFasten);
            double WidthFasten = _parameters.GetParameter(ParameterType.WidthFasten);
            double DiameterHandle = _parameters.GetParameter(ParameterType.DiameterHandle);

            double ChangeDiameterFasten = DiameterHandle * 2;
            // Создаем эскиз для первого диска на расстоянии 120 от начала координат
            var sketch1 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, -75 - WidthFasten);
            var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
            document2d_1.ksCircle(0, 0, ChangeDiameterFasten, 1); // Внешний круг радиусом 200

            sketch1.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            _wrapper.CreateExtrusion(sketch1, WidthFasten, false);





            var sketch2 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 75);
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(0, 0, ChangeDiameterFasten, 1); // Внешний круг радиусом 200

            sketch2.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            _wrapper.CreateExtrusion(sketch2, WidthFasten, false);

        }

    }
}

