namespace DumbellPlugin.Model
{
    using System;
    using System.Runtime.InteropServices;
    using Kompas6API5;
    using Kompas6Constants3D;

    /// <summary>
    /// Класс-обертка для работы с KOMPAS-3D.
    /// </summary>
    public class Wrapper
    {
        /// <summary>
        /// Компонент исполнения.
        /// </summary>
        private ksPart? _part;

        /// <summary>
        /// Получает объект KOMPAS-3D.
        /// </summary>
        public KompasObject? Kompas { get; private set; }

        /// <summary>
        /// Подключается к активной сессии KOMPAS-3D.
        /// </summary>
        /// <returns>True, если подключение успешно.
        /// В противном случае - false.</returns>
        public bool ConnectToKompas()
        {
            try
            {
                Kompas = (KompasObject)Activator.CreateInstance(Type.GetTypeFromProgID("KOMPAS.Application.5"));
                Console.WriteLine("Уже подключено к активной сессии KOMPAS-3D");
                return true;
            }
            catch (COMException)
            {
                try
                {
                    Kompas = (KompasObject)Activator.CreateInstance(
                        Type.GetTypeFromProgID("KOMPAS.Application.5"));
                    Kompas.Visible = true;
                    Kompas.ActivateControllerAPI();

                    Console.WriteLine("Успешно подключено к активной сессии KOMPAS-3D");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка при подключении к активной сессии KOMPAS-3D: " + ex.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неожиданная ошибка: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Создает 3D-документ KOMPAS-3D.
        /// </summary>
        /// <returns>Объект 3D-документа KOMPAS-3D.</returns>
        public ksDocument3D CreateDocument3D()
        {
            ksDocument3D document3D = Kompas.Document3D();

            document3D.Create();
            _part = document3D.GetPart((int)Part_Type.pTop_Part);

            return document3D;
        }

        /// <summary>
        /// Создает смещенную плоскость относительно другой плоскости.
        /// </summary>
        /// <param name="plane">Тип базовой плоскости.</param>
        /// <param name="offset">Величина смещения.</param>
        /// <returns>Экземпляр смещенной плоскости.</returns>
        public ksEntity CreateOffsetPlane(ksEntity offsetWidthEntity, double width, bool v, Obj3dType plane, double offset)
        {
            var offsetEntity = (ksEntity)_part.
                NewEntity((short)Obj3dType.o3d_planeOffset);
            var offsetDef = (ksPlaneOffsetDefinition)offsetEntity.
                GetDefinition();

            offsetDef.SetPlane((ksEntity)_part.NewEntity((short)plane));
            offsetDef.offset = offset;
            offsetDef.direction = false;
            offsetEntity.Create();

            return offsetEntity;
        }

        /// <summary>
        /// Создает эскиз на заданной плоскости.
        /// </summary>
        /// <param name="planeType">Тип плоскости, на которой создается
        /// эскиз.</param>
        /// <param name="offsetPlane">Смещенная плоскость
        /// (может быть null).</param>
        /// <returns>Определение созданного эскиза.</returns>
        public ksSketchDefinition CreateSketch(
        Obj3dType planeType,
        ksEntity offsetPlane)
        {
            var plane = (ksEntity)_part.
                GetDefaultEntity((short)planeType);
            var sketch = (ksEntity)_part.
                NewEntity((short)Obj3dType.o3d_sketch);
            var ksSketch = (ksSketchDefinition)sketch.GetDefinition();

            if (offsetPlane != null)
            {
                ksSketch.SetPlane(offsetPlane);
            }
            else
            {
                ksSketch.SetPlane(plane);
            }

            sketch.Create();

            // Важно вызвать EndEdit для завершения редактирования эскиза
           // sketch.EndEdit();

            return ksSketch;
        }


        /// <summary>
        /// Создает выдавливание на основе эскиза.
        /// </summary>
        /// <param name="sketch">Эскиз, на основе которого создается
        /// выдавливание.</param>
        /// <param name="depth">Глубина выдавливания.</param>
        /// <param name="side">Направление выдавливания
        /// (true - в одну сторону, false - в обратную).</param>
        /// <returns>Определение созданного выдавливания.</returns>
        public ksBossExtrusionDefinition CreateExtrusion(
            ksSketchDefinition sketch, double depth, bool side = true)
        {
            var extrusionEntity = (ksEntity)_part.
                NewEntity((short)ksObj3dTypeEnum.o3d_bossExtrusion);
            var extrusionDef = (ksBossExtrusionDefinition)extrusionEntity.
                GetDefinition();

            extrusionDef.SetSideParam(side, (short)End_Type.etBlind, depth);
            extrusionDef.directionType =
                side ? (short)Direction_Type.dtNormal :
                    (short)Direction_Type.dtReverse;
            extrusionDef.SetSketch(sketch);
            extrusionEntity.Create();

            return extrusionDef;
        }

        /// <summary>
        /// Создает выдавливание до ближайшей поверхности на основе эскиза.
        /// </summary>
        /// <param name="sketch">Эскиз, на основе которого создается
        /// выдавливание.</param>
        /// <param name="side">Направление выдавливания
        /// (true - в одну сторону, false - в обратную).</param>
        /// <returns>Определение созданного выдавливания.</returns>
        public ksBossExtrusionDefinition CreateExtrusionToNearSurface(
            ksSketchDefinition sketch,
            bool side = true)
        {
            var extrusionEntity = (ksEntity)_part.
                NewEntity((short)ksObj3dTypeEnum.o3d_bossExtrusion);
            var extrusionDef = (ksBossExtrusionDefinition)extrusionEntity.
                GetDefinition();

            extrusionDef.SetSideParam(
                side,
                (short)End_Type.etUpToNearSurface);
            extrusionDef.directionType =
                side ? (short)Direction_Type.dtNormal :
                    (short)Direction_Type.dtReverse;
            extrusionDef.SetSketch(sketch);
            extrusionEntity.Create();

            return extrusionDef;
        }

        internal ksSketchDefinition CreateSketch(ksEntity offsetEntity1, ksEntity offsetEntity2)
        {
            throw new NotImplementedException();
        }

        internal object CreateMirroredInstance(ksBossExtrusionDefinition extrusionDef, ksEntity offsetWidthEntity)
        {
            throw new NotImplementedException();
        }

        internal ksEntity CreateOffsetPlane(ksEntity offsetWidthEntity, double width, bool v1, bool v2, bool v3)
        {
            throw new NotImplementedException();
        }

        internal ksEntity CreateOffsetPlane(Obj3dType o3d_planeXOY, int v)
        {
            throw new NotImplementedException();
        }

        internal ksEntity CreateOffsetPlane(Obj3dType o3d_planeXOZ)
        {
            throw new NotImplementedException();
        }

        internal ksSketchDefinition CreateSketch(Obj3dType o3d_planeXOZ, double lengthHandle)
        {
            throw new NotImplementedException();
        }
    }
}