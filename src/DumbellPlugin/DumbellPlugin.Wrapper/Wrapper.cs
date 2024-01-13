// <copyright file="Wrapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Wrapper
{
    using System;
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
        /// Gets получает объект KOMPAS-3D.
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
                this.Kompas = Activator.CreateInstance(Type.
                    GetTypeFromProgID("KOMPAS.Application.5")) as KompasObject;

                if (this.Kompas != null)
                {
                    this.Kompas.Visible = true;
                    this.Kompas.ActivateControllerAPI();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Создает 3D-документ KOMPAS-3D.
        /// </summary>
        /// <returns>Объект 3D-документа KOMPAS-3D.</returns>
        public ksDocument3D CreateDocument3D()
        {
            ksDocument3D? document3D = this.Kompas?.Document3D() as ksDocument3D;

            if (document3D != null)
            {
                document3D.Create();
                this._part = (ksPart?)document3D.GetPart((int)Part_Type.pTop_Part);
            }

            return document3D;
        }

        /// <summary>
        /// Создайтется плоскость.
        /// </summary>
        /// <param name="planeType">Параметр1.</param>
        /// <param name="offset">Параметр2.</param>
        /// <returns>offsetEntity.</returns>
        public ksEntity CreateOffsetPlane(Obj3dType planeType, double offset)
        {
            var offsetEntity = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_planeOffset);
            var offsetDef = (ksPlaneOffsetDefinition)offsetEntity.GetDefinition();

            ksEntity planeEntity = (ksEntity)this._part.NewEntity((short)planeType);
            offsetDef.SetPlane(planeEntity);

            offsetDef.offset = offset;
            offsetDef.direction = false;
            offsetEntity.Create();

            // Возвращаем созданный объект смещенной плоскости
            return offsetEntity;
        }

        /// <summary>
        /// Создает эскиз.
        /// </summary>
        /// <param name="planeType">Параметр1.</param>
        /// <param name="offset">Параметр2.</param>
        /// <returns>ksketch.</returns>
        public ksSketchDefinition CreateSketch(Obj3dType planeType, double? offset)
        {
            ksEntity planeEntity;

            if (offset.HasValue)
            {
                // Создаем смещенную плоскость
                planeEntity = this.CreateOffsetPlane(planeType, offset.Value);
            }
            else
            {
                // Берем дефолтную плоскость
                planeEntity = (ksEntity)_part.GetDefaultEntity((short)planeType);
            }

            // Далее создаем эскиз
            var sketch = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_sketch);

            var ksSketch = (ksSketchDefinition)sketch.GetDefinition();

            ksSketch.SetPlane(planeEntity);

            sketch.Create();

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
    }
}