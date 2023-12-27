namespace DumbellPlugin.Model
{
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
        private readonly Parameters _parameters = new Parameters();

        /// <summary>
        /// 
        /// </summary>
        public Builder(Parameters parameters)
        {
            _parameters = parameters;
        }

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
        public void BuildDetail(object v)
        {
            _wrapper.ConnectToKompas();
            _wrapper.CreateDocument3D();

            BuildHandle();
            BuildDisksRight();
            BuildDisksLeft();
            /*BuildFastening();*/
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {
            /*if (_parameters is null)
            {
                return;
            }*/

            //double LengthHandle = _parameters.GetParameter(ParameterType.LengthHandle);

            // Создаем эскиз влево
            var sketchHandleL = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, -170);


            var document2d_L = (ksDocument2D)sketchHandleL.BeginEdit();
          
            // Создаем круг радиусом 50(радиус рукоятки)
            document2d_L.ksCircle(0, 0, 25, 1);
          

            sketchHandleL.EndEdit();
            

            // Теперь передаем этот эскиз в метод CreateExtrusion
            var extrusionDefL = _wrapper.CreateExtrusion(sketchHandleL, 340, false);

        }


        /// <summary>
        /// Строит диски детали.
        /// </summary>
        private void BuildDisksRight()
        {
            double AmountDisk = _parameters.GetParameter(ParameterType.AmountDisk);



            // Расстояние между дисками
            int offset = 25;

            for (int i = 0; i < AmountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch1 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 150 + i * offset);
                var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
                document2d_1.ksCircle(0, 0, 200, 1); // Внешний круг радиусом 200
                document2d_1.ksCircle(0, 0, 50, 1);  // Внутренний круг радиусом 50
                sketch1.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                _wrapper.CreateExtrusion(sketch1, 20, false);
            }

            /*// Создаем эскиз для второго диска на расстоянии -120 от начала координат
            var sketch2 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, AmountDisk);
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(-120, 0, 200, 1); // Внешний круг радиусом 200
            document2d_2.ksCircle(-120, 0, 50, 1);  // Внутренний круг радиусом 50
            sketch2.EndEdit();

            // Выдавливаем второй диск в отрицательном направлении на 20
            _wrapper.CreateExtrusion(sketch2, -20, false);*/
        }

        private void BuildDisksLeft() 
        {
            // Расстояние между дисками
            int offset = 25;
            double AmountDisk = _parameters.GetParameter(ParameterType.AmountDisk);

            for (int i = 0; i < AmountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch2 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, -170 - i * offset);
                var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
                document2d_2.ksCircle(0, 0, 200, 1); // Внешний круг радиусом 200
                document2d_2.ksCircle(0, 0, 50, 1);  // Внутренний круг радиусом 50
                sketch2.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                _wrapper.CreateExtrusion(sketch2, 20, false);
            }
            /*// Создаем эскиз для второго диска на расстоянии -120 от начала координат
            var sketch2 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, AmountDisk);
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(-120, 0, 200, 1); // Внешний круг радиусом 200
            document2d_2.ksCircle(-120, 0, 50, 1);  // Внутренний круг радиусом 50
            sketch2.EndEdit();

            // Выдавливаем второй диск в отрицательном направлении на 20
            _wrapper.CreateExtrusion(sketch2, -20, false);*/

        }

    }
}

/*/// <summary>
/// Строит крепления.
/// </summary>
private void BuildFastening()
{
    var offsetWidthEntity = _wrapper.CreateOffsetPlane(
        Obj3dType.o3d_planeXOY,
        LayerHeight - ParametersDict[ParameterType.FoundationThickness]);
    var sketch = _wrapper.
        CreateSketch(Obj3dType.o3d_planeXOY, offsetWidthEntity);
    var document2d = (ksDocument2D)sketch.BeginEdit();

    document2d.ksCircle(0, 0, _fasteningRadius, 1);
    sketch.EndEdit();

    _wrapper.CreateExtrusionToNearSurface(sketch, false);
}
}
}*/