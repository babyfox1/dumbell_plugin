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
            BuildDisks();
            /*BuildFastening();*/
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {
            if (_parameters is null)
            {
                return;
            }

            double LengthHandle = _parameters.GetParameter(ParameterType.LengthHandle);

            // Создаем эскиз
            var sketch = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, LengthHandle);

            var document2d = (ksDocument2D)sketch.BeginEdit();

            // Создаем круг радиусом 100
            document2d.ksCircle(0, 0, 100, 1);

            sketch.EndEdit();

            // Теперь передаем этот эскиз в метод CreateExtrusion
            var extrusionDef = _wrapper.CreateExtrusion(sketch, LengthHandle, false);

            // Выдавливаем круг в отрицательном направлении на 120
            _wrapper.CreateExtrusion(sketch, -1 * LengthHandle, false);
        }


        /// <summary>
        /// Строит диски детали.
        /// </summary>
        private void BuildDisks()
        {
            double AmountDisk = _parameters.GetParameter(ParameterType.AmountDisk);

            var sketch = _wrapper.CreateSketch(Obj3dType.o3d_planeXOY, AmountDisk);

            var document2d = (ksDocument2D)sketch.BeginEdit();

            // Внешний круг радиусом 200
            document2d.ksCircle(150, 0, 200, 1);

            // Внутренний круг радиусом 55
            document2d.ksCircle(150, 0, 55, 1);

            sketch.EndEdit();

            // Выдавливаем соединенные круги в положительном направлении на 20
            var extrusionDef = _wrapper.CreateExtrusion(sketch, 20, false);

            // Выдавливаем в отрицательном направлении на 20
            _wrapper.CreateExtrusion(sketch, -20, false);
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