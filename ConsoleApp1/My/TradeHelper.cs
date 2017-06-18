//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using TSLab.DataSource;
//using TSLab.Script;
//using System.Linq;
//using TSLab.Script.Handlers;

//namespace My
//{
//    /// <summary>
//    /// Вспомогательный класс, содержащий хелпер методы. В том числе и методы расширения.
//    /// </summary>
//    public static class TradeHelper
//    {
//        #region Свечные хелперы

//        /// <summary>
//        /// Возвращает истину если свечка правильная. Хай лоу опен клоуз согласованы.Хай не ниже лоя например.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsGood(this BaseBar candle)
//        {
//            if (candle.High < candle.Low)
//                return false;

//            if (candle.High < candle.Open)
//                return false;

//            if (candle.High < candle.Close)
//                return false;

//            if (candle.Low > candle.Open)
//                return false;

//            if (candle.Low > candle.Close)
//                return false;

//            return true;
//        }

//        /// <summary>
//        /// Возвращает истину если цена закрытия больше цены открытия.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsWhite(this BaseBar candle)
//        {
//            return candle.Close > candle.Open;
//        }

//        /// <summary>
//        /// По списку свечей возвращает список логических значений. Если свеча белая, то истину. Иначе ложь.
//        /// Упрощает работу со списками.
//        /// </summary>
//        /// <param name="candles"></param>
//        /// <returns></returns>
//        public static IList<bool> IsWhite(this IList<Bar> candles)
//        {
//            var values = new bool[candles.Count];

//            for (var i = 0; i < candles.Count; i++)
//                values[i] = candles[i].IsWhite();

//            return values;
//        }

//        /// <summary>
//        /// Возвращает истину если цена закрытия меньше цены открытия.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsBlack(this BaseBar candle)
//        {
//            return candle.Close < candle.Open;
//        }

//        /// <summary>
//        /// Возвращает истину если цена открытия равна цене закрытия.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsDodj(this BaseBar candle)
//        {
//            return candle.Close.IsPriceEqual(candle.Open);
//        }

//        /// <summary>
//        /// Возвращает истину если свеча имеет тело без теней.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsMaribozu(this BaseBar candle)
//        {
//            return candle.NoHighShadow() && candle.NoLowShadow();
//        }

//        /// <summary>
//        /// Возвращает истину если свеча имеет тело без теней и свеча белая.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsMaribozuWhite(this BaseBar candle)
//        {
//            return candle.IsWhite() && candle.NoHighShadow() && candle.NoLowShadow();
//        }

//        /// <summary>
//        /// Возвращает истину если свеча имеет тело без теней и свеча черная.
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool IsMaribozuBlack(this BaseBar candle)
//        {
//            return candle.IsBlack() && candle.NoHighShadow() && candle.NoLowShadow();
//        }

//        /// <summary>
//        /// Возвращает истину если нет верхней тени
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool NoHighShadow(this BaseBar candle)
//        {
//            if (candle.IsWhite())
//                return candle.High.IsPriceEqual(candle.Close);

//            if (candle.IsBlack())
//                return candle.High.IsPriceEqual(candle.Open);

//            if (candle.IsDodj())
//                return candle.High.IsPriceEqual(candle.Open);

//            return false;
//        }

//        /// <summary>
//        /// Возвращает истину если нет нижней тени
//        /// </summary>
//        /// <param name="candle"></param>
//        /// <returns></returns>
//        public static bool NoLowShadow(this BaseBar candle)
//        {
//            if (candle.IsWhite())
//                return candle.Low.IsPriceEqual(candle.Open);

//            if (candle.IsBlack())
//                return candle.Low.IsPriceEqual(candle.Close);

//            if (candle.IsDodj())
//                return candle.Low.IsPriceEqual(candle.Open);

//            return false;
//        }

//        #endregion

//        #region Индикаторные хелперы        

//        /// <summary>
//        /// Возвращает истину если на заданном баре первая кривая пересекает вторую вверх.
//        /// </summary>
//        /// <param name="list0">Первая кривая</param>
//        /// <param name="list1">Вторая кривая</param>
//        /// <param name="bar">Номер бара на котором искать пересечение</param>
//        /// <returns></returns>
//        public static bool CrossUp(this IList<double> list0, IList<double> list1, int bar)
//        {
//            if ((list0[bar - 1] <= list1[bar - 1]) && (list0[bar] > list1[bar]))
//                return true;

//            return false;
//        }

//        /// <summary>
//        /// Возвращает истину если на заданном баре первая кривая пересекает вторую вниз.
//        /// </summary>
//        /// <param name="list0">Первая кривая</param>
//        /// <param name="list1">Вторая кривая</param>
//        /// <param name="bar">Номер бара на котором искать пересечение</param>
//        /// <returns></returns>
//        public static bool CrossDown(this IList<double> list0, IList<double> list1, int bar)
//        {
//            return CrossUp(list1, list0, bar);
//        }

//        /// <summary>
//        /// Расчитывает направление кривой. Варианты: вверх, вниз и прямо.
//        /// Возвращает список направлений. 
//        /// Если предыдущее меньше текущего значения, то вверх = 1 
//        /// Если предыдущее больше текущего, то вниз = -1
//        /// Если текущее равно предыдущему то равно = 0
//        /// </summary>
//        /// <param name="list"></param>
//        /// <returns></returns>
//        public static IList<double> Directions(this IList<double> list)
//        {
//            if (list.Count < 2)
//                throw new ArgumentException("Число элементов в списке должно быть 2 или больше.");

//            var directions = new double[list.Count];
//            directions[0] = 0;
//            for (var i = 1; i < list.Count; i++)
//            {
//                if (list[i] > list[i - 1])
//                    directions[i] = 1;
//                else if (list[i] < list[i - 1])
//                    directions[i] = -1;
//                else
//                    directions[i] = 0;
//            }

//            return directions;
//        }

//        /// <summary>
//        /// Расчитывает все точки пересечения двух кривых. Пересечение вверх считается через <see cref="CrossUp"/>,
//        /// а пересечение вниз считается через <see cref="CrossDown"/>.
//        /// Если вверх то 1, если вниз то -1, все другие случаи 0.
//        /// Кривая <see cref="list0"/> пересекает кривую <see cref="list1"/>
//        /// </summary>
//        /// <param name="list0"></param>
//        /// <param name="list1"></param>
//        /// <returns></returns>
//        public static IList<double> Crossings(this IList<double> list0, IList<double> list1)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list0.Count != list1.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            if (list0.Count < 2)
//                throw new ArgumentException("Число элементов в списке должно быть 2 или больше.");

//            var crossing = new double[list0.Count];
//            for (var i = 1; i < list0.Count; i++)
//            {
//                if (list0.CrossUp(list1, i))
//                    crossing[i] = 1;
//                else if (list0.CrossDown(list1, i))
//                    crossing[i] = -1;
//                else
//                    crossing[i] = 0;
//            }
//            return crossing;
//        }

//        /// <summary>
//        /// Подсчитывается число пересечений двух кривых в заданном диапазоне. В каждом элементе списка имеем число
//        /// пересечений за предыдущие period баров. 
//        /// </summary>
//        /// <param name="list0">Список 1</param>
//        /// <param name="list1">Список 2</param>
//        /// <param name="period">Период в котором ищем число пересечений. Число не меньше 1.</param>
//        /// <returns></returns>
//        public static IList<int> CrossCount(this IList<double> list0, IList<double> list1, int period)
//        {
//            if (period < 1)
//                throw new ArgumentOutOfRangeException("period", "период должен быть положительным числом больше 0.");

//            if (list0.Count != list1.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            var result = new int[list0.Count];

//            var crossings = list0.Crossings(list1);
//            var counter = 0;
//            for (var i = 0; i < list0.Count; i++)
//            {
//                counter += Math.Abs((int)crossings[i]);

//                if (i >= period)
//                    counter -= Math.Abs((int)crossings[i - period]);

//                result[i] = counter;
//            }

//            return result;
//        }

//        /// <summary>
//        /// Производит вычитание двух коллекций. 
//        /// Из первой вычитает вторую и возвращает коллекцию с элементами равными разности элементов коллекций 1 и 2.
//        /// Если колллекции разной длины, то вернет null.
//        /// </summary>
//        /// <param name="list">Коллекция из которой вычитать</param>
//        /// <param name="subtrList">Колллекция которую будет вычитать.</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentException">Если списки разной длины.</exception>
//        public static IList<double> Subtract(this IList<double> list, IList<double> subtrList)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list.Count != subtrList.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            // Создаем массив, и забиваем его разностями элементов списков.
//            var res = new double[list.Count];
//            for (var i = 0; i < list.Count; i++)
//                res[i] = list[i] - subtrList[i];


//            return res;
//        }

//        /// <summary>
//        /// Производит вычитание двух коллекций. 
//        /// Из первой вычитает вторую и возвращает коллекцию с элементами равными разности элементов коллекций 1 и 2.
//        /// Если колллекции разной длины, то вернет null.
//        /// </summary>
//        /// <param name="list">Коллекция из которой вычитать</param>
//        /// <param name="subtrList">Колллекция которую будет вычитать.</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentException">Если списки разной длины.</exception>
//        public static IList<int> Subtract(this IList<int> list, IList<int> subtrList)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list.Count != subtrList.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            // Создаем массив, и забиваем его разностями элементов списков.
//            var res = new int[list.Count];
//            for (var i = 0; i < list.Count; i++)
//                res[i] = list[i] - subtrList[i];


//            return res;
//        }

//        /// <summary>
//        /// Производит сложение двух коллекций. 
//        /// К первой коллекции прибавляет вторую и возвращает коллекцию с элементами равными сумме элементов коллекций 1 и 2.
//        /// Если колллекции разной длины, то вернет null.
//        /// </summary>
//        /// <param name="list">Коллекция к которой прибавлять</param>
//        /// <param name="subtrList">Колллекция которую будем прибавлять.</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentException">Если списки разной длины.</exception>
//        public static IList<double> Add(this IList<double> list, IList<double> subtrList)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list.Count != subtrList.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            // Создаем массив, и забиваем его суммами элементов списков.
//            var res = new double[list.Count];
//            for (var i = 0; i < list.Count; i++)
//                res[i] = list[i] + subtrList[i];


//            return res;
//        }

//        /// <summary>
//        /// Производит сложение двух коллекций. 
//        /// К первой коллекции прибавляет вторую и возвращает коллекцию с элементами равными сумме элементов коллекций 1 и 2.
//        /// Если колллекции разной длины, то вернет null.
//        /// </summary>
//        /// <param name="list">Коллекция к которой прибавлять</param>
//        /// <param name="subtrList">Колллекция которую будем прибавлять.</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentException">Если списки разной длины.</exception>
//        public static IList<int> Add(this IList<int> list, IList<int> subtrList)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list.Count != subtrList.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            // Создаем массив, и забиваем его суммами элементов списков.
//            var res = new int[list.Count];
//            for (var i = 0; i < list.Count; i++)
//                res[i] = list[i] + subtrList[i];

//            return res;
//        }



//        /// <summary>
//        /// Универсальный метод сложения. Складывает два списка разных объектов. Возвращает список сумм.
//        /// </summary>
//        /// <typeparam name="T">Тип исходного списка.</typeparam>
//        /// <param name="list">Список к которому прибавлять</param>
//        /// <param name="secList">Список который прибавлять</param>
//        /// <param name="selector">Селектор</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentException">Если списки разной длины.</exception>
//        public static IList<double> Add<T>(this IList<T> list, IList<T> secList, Func<T, double> selector)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list.Count != secList.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            var res = new double[list.Count];

//            // Заполняем массив суммами, для выбора элемента который суммировать используем селектор.
//            // Включаем контроль переполнения типа.
//            for (var i = 0; i < list.Count; i++)
//                checked
//                {
//                    res[i] = selector(list[i]) + selector(secList[i]);
//                }

//            return res;
//        }

//        /// <summary>
//        /// Универсальный метод вычитания. Вычитает два списка разных объектов. Возвращает список разностей.
//        /// </summary>
//        /// <typeparam name="T">Тип исходного списка.</typeparam>
//        /// <param name="list">Список из которого вычитать</param>
//        /// <param name="secList">Список который вычитать</param>
//        /// <param name="selector">Селектор</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentException">Если списки разной длины.</exception>
//        public static IList<double> Subtract<T>(this IList<T> list, IList<T> secList, Func<T, double> selector)
//        {
//            // Если длины коллекций различаются то просто вернем null как знак ошибки.
//            if (list.Count != secList.Count)
//                throw new ArgumentException("Списки должны быть одинаковой длины");

//            var res = new double[list.Count];

//            // Заполняем массив разностями, для выбора элемента который вычитать используем селектор.
//            // Включаем контроль переполнения типа.
//            for (var i = 0; i < list.Count; i++)
//                checked
//                {
//                    res[i] = selector(list[i]) - selector(secList[i]);
//                }


//            return res;
//        }

//        /// <summary>
//        /// По входному списку вычисляет приращения, и возвращает список приращений.
//        /// </summary>
//        /// <param name="list"></param>
//        /// <returns></returns>
//        public static IList<double> IncrementsPcnt(this IList<double> list)
//        {
//            if (list.Count < 2)
//                throw new ArgumentException("Число элементов в списке должно быть 2 или больше.");

//            var result = new double[list.Count];
//            result[0] = 0;

//            // Если предыдущее значение равно 0, то найти приращение не представляется возможным. 
//            // Вернем число 0. То есть не было приращения. Если сначала шли одни нули, то приращения появятся только через 1 бар после того как нули пропадут
//            for (var i = 1; i < list.Count; i++)
//                result[i] = list[i - 1] == 0 ? 0 : (list[i] - list[i - 1]) / list[i - 1];

//            return result;
//        }



//        /// <summary>
//        /// По заданному списку элементов ищет фрактал в центре. Слева можно иметь такой же экстремум, справа равенство недопускается.
//        /// </summary>
//        /// <param name="list"></param>
//        /// <returns></returns>
//        public static bool IsHighFractal(this IList<double> list)
//        {
//            if (list.Count % 2 == 0)
//                throw new InvalidOperationException("Нельзя найти фрактал по четному числу элементов.");

//            var centerIndex = (int)Math.Truncate(list.Count / 2.0);
//            var centerItem = list[centerIndex];

//            // перебираем левую часть. В ней может быть меньше или равно. Больше нельзя.
//            for (var i = 0; i < centerIndex; i++)
//            {
//                if (list[i] > centerItem)
//                    return false;
//            }

//            // перебираем правую часть. В ней может быть меньше. Равно нельзя.
//            for (var i = centerIndex + 1; i < list.Count; i++)
//            {
//                if (list[i] >= centerItem)
//                    return false;
//            }

//            return true;
//        }

//        public static bool IsLowFractal(this IList<double> list)
//        {
//            if (list.Count % 2 == 0)
//                throw new InvalidOperationException("Нельзя найти фрактал по четному числу элементов.");

//            var centerIndex = (int)Math.Truncate(list.Count / 2.0);
//            var centerItem = list[centerIndex];

//            // перебираем левую часть. В ней может быть меньше или равно. Больше нельзя.
//            for (var i = 0; i < centerIndex; i++)
//            {
//                if (list[i] < centerItem)
//                    return false;
//            }

//            // перебираем правую часть. В ней может быть меньше. Равно нельзя.
//            for (var i = centerIndex + 1; i < list.Count; i++)
//            {
//                if (list[i] <= centerItem)
//                    return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// В заданном списке ищет фрактал в заданной точке с заданным периодом.
//        /// Если данных не хватает, то выдает false
//        /// </summary>
//        /// <param name="list">Список значений</param>
//        /// <param name="bar">Номер значения которое подозреватеся на фрактал</param>
//        /// <param name="period">период фрактала</param>
//        /// <returns></returns>
//        public static bool IsHighFractal(this IList<double> list, int bar, int period)
//        {
//            // не может быть фрактала меньше чем 5 размерности
//            if (period.IsEven() || period < 5)
//                throw new ArgumentException("Период фрактала должен быть НЕ четным и больше 5.", "period");

//            // вычисляем базовые величины
//            var cnt = list.Count;
//            var maxInd = cnt - 1;
//            var lever = (int)(period / 2);       // отбросится дробная часть при приведении

//            // проверяем сначала граничные условия. Слева и справа от bar должно хватать свечек
//            if (bar > maxInd || bar < 0)
//                throw new InvalidOperationException("Невозможно найти фрактал за пределами списка.");


//            var centerIndex = bar;
//            var centerItem = list[bar];

//            // если границы фрактала выходят за край списка, вернем ЛОЖЬ и все.
//            if (centerIndex + lever > maxInd || centerIndex - lever < 0)
//                return false;

//            // перебираем левую часть. В ней может быть меньше или равно. Больше нельзя.
//            for (var i = centerIndex - lever; i < centerIndex; i++)
//            {
//                if (list[i] > centerItem)
//                    return false;

//            }

//            // перебираем правую часть. В ней может быть меньше. Равно нельзя.
//            for (var i = centerIndex + 1; i <= centerIndex + lever; i++)
//            {
//                if (list[i] >= centerItem)
//                    return false;
//            }

//            return true;
//        }

//        public static bool IsLowFractal(this IList<double> list, int bar, int period)
//        {
//            // не может быть фрактала меньше чем 5 размерности
//            if (period.IsEven() || period < 5)
//                throw new ArgumentException("Период фрактала должен быть НЕ четным и больше 5.", "period");

//            // вычисляем базовые величины
//            var cnt = list.Count;
//            var maxInd = cnt - 1;
//            var lever = (int)(period / 2);       // отбросится дробная часть при приведении

//            // проверяем сначала граничные условия. Слева и справа от bar должно хватать свечек
//            if (bar > maxInd || bar < 0)
//                throw new InvalidOperationException("Невозможно найти фрактал за пределами списка.");


//            var centerIndex = bar;
//            var centerItem = list[bar];

//            // если границы фрактала выходят за край списка, вернем ЛОЖЬ и все.
//            if (centerIndex + lever > maxInd || centerIndex - lever < 0)
//                return false;

//            // перебираем левую часть. В ней может быть больше или равно. Меньше нельзя.
//            for (var i = centerIndex - lever; i < centerIndex; i++)
//            {
//                if (list[i] < centerItem)
//                    return false;
//            }

//            // перебираем правую часть. В ней может быть больше. Равно нельзя.
//            for (var i = centerIndex + 1; i <= centerIndex + lever; i++)
//            {
//                if (list[i] <= centerItem)
//                    return false;
//            }

//            return true;
//        }

//        /// <summary>
//        /// Берет в списке последние бары lastBarsCnt и среди них ищет фракталы. Возвращает индексы фрактальных свечек
//        /// </summary>
//        /// <param name="list"></param>
//        /// <param name="lastBarsCnt">число последних бар</param>
//        /// <param name="fractPeriod">период фракталов</param>
//        /// <returns>Возвращает индексы фрактальных свечек или пустой список</returns>
//        public static IList<int> GetLastHighFractalsIndexes(this IList<double> list, int lastBarsCnt, int fractPeriod)
//        {
//            var cnt = list.Count;
//            var lastIndex = cnt - 1;
//            var firstIndex = lastIndex - lastBarsCnt;
//            var lever = (int)(fractPeriod / 2);       // плечо фрактала. Отбросится дробная часть при приведении

//            if (lastBarsCnt > cnt)
//                throw new ArgumentException("Значение аргумента не должно быть больше общего числа бар.", "lastBarsCnt");

//            var result = new List<int>();

//            // сразу пропустим часть баров, чтобы искать фрактал ТОЛЬКО в пределах заданных границ.
//            for (var i = firstIndex + lever; i <= lastIndex; i++)
//            {
//                if (list.IsHighFractal(i, fractPeriod))
//                    result.Add(i);
//            }

//            return result;
//        }

//        public static IList<int> GetLastLowFractalsIndexes(this IList<double> list, int lastBarsCnt, int fractPeriod)
//        {
//            var cnt = list.Count;
//            var lastIndex = cnt - 1;
//            var firstIndex = lastIndex - lastBarsCnt;
//            var lever = (int)(fractPeriod / 2);       // плечо фрактала. Отбросится дробная часть при приведении

//            if (lastBarsCnt > cnt)
//                throw new ArgumentException("Значение аргумента не должно быть больше общего числа бар.", "lastBarsCnt");

//            var result = new List<int>();

//            // сразу пропустим часть баров, чтобы искать фрактал ТОЛЬКО в пределах заданных границ.
//            for (var i = firstIndex + lever; i <= lastIndex; i++)
//            {
//                if (list.IsLowFractal(i, fractPeriod))
//                    result.Add(i);
//            }

//            return result;
//        }

//        #endregion

//        #region Общие хелперы

//        public static bool IsNull(this object obj)
//        {
//            return obj == null;
//        }

//        /// <summary>
//        /// Метод упрощающий форматирование строк.
//        /// </summary>
//        /// <param name="str">Строка для форматирования.</param>
//        /// <param name="args">Аргументы для форматирования.</param>
//        /// <returns></returns>
//        public static string Put(this string str, params object[] args)
//        {
//            // Вводим культуру для того чтобы double числа не переводились как числа с запятой! Это гадит.
//            return string.Format(CultureInfo.InvariantCulture, str, args);
//        }

//        /// <summary>
//        /// Сравнивает два значения double. Нужен для сравнения цен. Использует дельту 1Е-10
//        /// Если разница между двумя значениями меньше дельты вернет истину.
//        /// </summary>
//        /// <param name="d1"></param>
//        /// <param name="d2"></param>
//        /// <returns></returns>
//        public static bool IsPriceEqual(this double d1, double d2)
//        {
//            return Math.Abs(d1 - d2) < 1E-10;
//        }

//        /// <summary>
//        /// Выводит в лог информационное сообщение.
//        /// </summary>
//        /// <param name="ctx"></param>
//        /// <param name="str"></param>
//        /// <param name="args"></param>
//        public static void LogInfo(this IContext ctx, string str, params object[] args)
//        {
//            // Задаем цвет соообщения
//            var color = System.Drawing.Color.Blue;

//            // Формируем строку, вставляем спец слово Info.
//            var msg = string.Format("Info: " + str, args);

//            ctx.Log(msg, new Color(color.ToArgb()));
//        }

//        /// <summary>
//        /// Выводит в лог сообщение об ошибке.
//        /// </summary>
//        /// <param name="ctx"></param>
//        /// <param name="str"></param>
//        /// <param name="args"></param>
//        public static void LogError(this IContext ctx, string str, params object[] args)
//        {
//            // Задаем цвет соообщения
//            var color = System.Drawing.Color.Red;

//            // Формируем строку, вставляем спец слово Info.
//            var msg = string.Format("Error: " + str, args);

//            ctx.Log(msg, new Color(color.ToArgb()));
//        }

//        #endregion

//        #region Хелперы позиций

//        /// <summary>
//        /// Комиссия позиции на открытие.
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static double OpenCommission(this IPosition pos)
//        {
//            var sec = pos.Security;

//            // Проверим задан ли экземпляр делегата.
//            if (sec.Commission == null)
//                return 0;

//            return sec.Commission(pos, true);
//        }

//        /// <summary>
//        /// Комиссия позиции на закрытие.
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static double CloseCommission(this IPosition pos)
//        {
//            var sec = pos.Security;

//            // Проверим задан ли экземпляр делегата.
//            if (sec.Commission == null)
//                return 0;

//            return sec.Commission(pos, false);
//        }

//        /// <summary>
//        /// Возвращает общую коммиссию по инструменту на заданный бар.
//        /// </summary>
//        /// <param name="sec"></param>
//        /// <param name="bar"></param>
//        /// <returns></returns>
//        public static double TotalCommission(this ISecurity sec, int bar)
//        {
//            // Получаем список всех позиций которые на заданном баре активны или уже были закрыты в прошлом.
//            // Далее суммируем коммиссии для каждой из этих позиций.
//            var posList = sec.Positions.GetClosedOrActiveForBar(bar);
//            var commission = posList.Sum(p => p.TotalCommission(bar));

//            return commission;
//        }

//        /// <summary>
//        /// Возвращает комиссию для позиции на заданный бар. Учитывает и открытые и закрытые позиции.
//        /// </summary>
//        /// <param name="position"></param>
//        /// <param name="bar"></param>
//        /// <returns></returns>
//        public static double TotalCommission(this IPosition position, int bar)
//        {
//            // Если позиция на заданный бар еще не закрыта, то посчитаем только комиссию за вход в позицию.
//            if (position.IsActiveForbar(bar))
//                return position.OpenCommission();
//            // Если позиция уже закрылась в прошлом, то считаем полную комиссию за вход и выход.
//            else
//                return position.OpenCommission() +
//                       position.CloseCommission();
//        }

//        /// <summary>
//        /// Возвращает общий профит в единицах валюты по всем позициям на заданной свече.
//        /// </summary>
//        /// <param name="positionsList">Список позиций</param>
//        /// <param name="bar">Номер свечи</param>
//        /// <returns></returns>
//        public static double TotalProfit(this IPositionsList positionsList, int bar)
//        {
//            // Выбираем только те позиции, которые либо еще не закрыты либо уже закрыты для заданного бара.
//            // Если все позиции брать подряд получим учет и будущих позиций тоже что неверно.
//            var positions = positionsList.GetClosedOrActiveForBar(bar);

//            // OpenProfit не учитывает комиссию на вход/выход из позиции.
//            return positions.Sum(p =>
//            {
//                if (p.IsActiveForbar(bar))
//                    return p.PositionOpenProfit(bar);
//                else
//                    return p.Profit();
//            });
//        }

//        /// <summary>
//        /// Расчитывает профит для полной позиции на заданном баре. Комиссия на вход НЕ учитывается.
//        /// </summary>
//        /// <param name="pos">Позиция</param>
//        /// <param name="bar">Бар</param>
//        /// <returns></returns>
//        public static double PositionOpenProfit(this IPosition pos, int bar)
//        {
//            return pos.OpenProfit(bar) * pos.PosSize();
//        }

//        /// <summary>
//        /// Расчитывает профит в % для полной позиции на заданном баре.
//        /// </summary>
//        /// <param name="pos">Позиция</param>
//        /// <param name="bar">Бар</param>
//        /// <returns></returns>
//        public static double PositionOpenProfitPct(this IPosition pos, int bar)
//        {
//            // Если мы считаем в процентах, то не важно сколько лотов и контрактов. Процент всегда одинаков.
//            return pos.OpenProfitPct(bar);
//        }

//        /// <summary>
//        /// Возвращает общую стоимость позиции на момент входа
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static double PositionEntryPrice(this IPosition pos)
//        {
//            return pos.EntryPrice * pos.PosSize();
//        }

//        /// <summary>
//        /// Если сделка была закрыта и открыта между клирингами она считается скальперской на РТС.
//        /// Учитывается дневной и вечерний клиринг.
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static bool IsScalp(this IPosition pos)
//        {
//            if (pos.IsActive)
//                throw new ArgumentException("Нельзя определить скальперская сделка или нет для НЕЗАКРЫТОЙ позиции.");

//            // НА ФОРТС если удержание было в течение одной сесси до клиринга, комиссия берется в два раза меньше.
//            // Учитываем число лотов и ВХОД + ВЫХОД. Комис применяется и туда и сюда. По факту наш комисс будет прописан в выходе из позиции
//            var dayClearingTime = new TimeSpan(14, 01, 00);
//            var eveningClearingTime = new TimeSpan(18, 50, 00);

//            var entryDate = pos.EntryBar.Date;
//            var exitDate = pos.ExitBar.Date;

//            // Если сделка была открыта и закрыта в между дневным и вечерним клирингом
//            var isScalp = (entryDate.Date == exitDate.Date)
//                             && (entryDate.TimeOfDay.InRange(dayClearingTime, eveningClearingTime)
//                             && (exitDate.TimeOfDay.InRange(dayClearingTime, eveningClearingTime)))

//                          // Если сделка была открыта и закрыта в между вечерним и дневным клирингом следующего дня
//                          // Если не попала сделка между дневным и вечерним клиром, тогда она попала между вечерним и дневным.
//                          || (entryDate.Date == exitDate.Date - TimeSpan.FromDays(1))
//                             && (entryDate.TimeOfDay.InRange(dayClearingTime, eveningClearingTime) == false
//                               && (exitDate.TimeOfDay.InRange(dayClearingTime, eveningClearingTime) == false));

//            return isScalp;
//        }

//        /// <summary>
//        /// Полный размер позиции в бумагах. Учитывается размер лота.
//        /// </summary>
//        /// <param name="pos"></param>
//        /// <returns></returns>
//        public static double PosSize(this IPosition pos)
//        {
//            return pos.Shares * pos.Security.LotSize;
//        }

//        /// <summary>
//        /// Возвращает среднюю цену входа для всех позиций из списка.
//        /// </summary>
//        /// <param name="positions">Спислк позиций для которых расчитать</param>
//        /// <returns></returns>
//        public static double AvgEntryPrice(this IList<IPosition> positions)
//        {
//            var totalPrice = positions.Sum(p => p.PositionEntryPrice());
//            var totalSize = positions.Sum(p => p.PosSize());

//            return totalPrice / totalSize;
//        }

//        /// <summary>
//        /// Возвращает общий размер списка позиций в контрактах/акциях. Учитывает размер лота и размер позы в лотах.
//        /// </summary>
//        /// <param name="positions">Спислк позиций для которых расчитать</param>
//        /// <returns></returns>
//        public static double TotalSize(this IEnumerable<IPosition> positions)
//        {
//            return positions.Sum(p => p.PosSize());
//        }

//        /// <summary>
//        /// Возвращает общий размер списка позиций в лотах.
//        /// </summary>
//        /// <param name="positions">Спислк позиций для которых расчитать</param>
//        /// <returns></returns>
//        public static double TotalSizeLots(this IEnumerable<IPosition> positions)
//        {
//            return positions.Sum(p => p.Shares);
//        }

//        /// <summary>
//        /// Возвращает цену одного лота инструмента на заданном баре.
//        /// </summary>
//        /// <param name="sec">Инструмент</param>
//        /// <param name="i">Бар, из цены закрытия которого вычисляется цена лота</param>
//        /// <returns></returns>
//        public static double LotPrice(this ISecurity sec, int i)
//        {
//            return sec.ClosePrices[i] * sec.LotSize;
//        }

//        #endregion

//        #region Хелперы времени/даты

//        /// <summary>
//        /// Возвращает истину если время лежит в заданных границах, ВКЛЮЧИТЕЛЬНО!
//        /// </summary>
//        /// <param name="time">Время</param>
//        /// <param name="minTime">Минимальная граница</param>
//        /// <param name="maxTime">Максимальная граница</param>
//        /// <returns></returns>
//        public static bool InRange(this TimeSpan time, TimeSpan minTime, TimeSpan maxTime)
//        {
//            return time >= minTime && time <= maxTime;
//        }

//        #endregion

//        #region Списки

//        public static void RemoveLast<T>(this IList<T> list)
//        {
//            list.RemoveAt(list.Count - 1);
//        }

//        public static void RemoveLast<T>(this IList<T> list, int count)
//        {
//            for (var i = 0; i < count; i++)
//                list.RemoveAt(list.Count - 1);
//        }

//        /// <summary>
//        /// Просто возвращает число, равное индексу последнего элемента списка/массива
//        /// </summary>
//        /// <param name="list"></param>
//        /// <exception cref="ArgumentException">если список не имеет элементов</exception>
//        /// <exception cref="ArgumentNullException">если список null</exception>
//        /// <returns></returns>
//        public static int GetLastIndex<T>(this IList<T> list)
//        {
//            if (list == null)
//                throw new ArgumentNullException("list");

//            if (list.Count == 0)
//                throw new ArgumentException("Список должен содержать члены.", "list");

//            return list.Count - 1;
//        }

//        /// <summary>
//        /// Ищем элемент удовлетворяющий условию и возвращает его индекс в списке.
//        /// </summary>
//        /// <typeparam name="T">Тип элементов списка</typeparam>
//        /// <param name="list">Список</param>
//        /// <param name="predicat">Предикат, проверяющий элементы</param>
//        /// <returns></returns>
//        /// <exception cref="ArgumentNullException">Если список равен null</exception>
//        /// <exception cref="ArgumentException">Если список равен пустой</exception>
//        public static int GetFirstIndex<T>(this IList<T> list, Func<T, bool> predicat)
//        {
//            if (list == null)
//                throw new ArgumentNullException("list");

//            if (list.Count == 0)
//                throw new ArgumentException("Список должен содержать члены.", "list");

//            for (var i = 0; i < list.Count; i++)
//            {
//                if (predicat(list[i]))
//                    return i;
//            }

//            return -1;
//        }

//        #endregion

//        #region Базовые типы данных

//        /// <summary>
//        /// Определяет является ли число бесконечностью
//        /// </summary>
//        /// <param name="d"></param>
//        /// <returns></returns>
//        public static bool IsInfinity(this double d)
//        {
//            return double.IsInfinity(d);
//        }

//        /// <summary>
//        /// Определяет является ли значение НЕ ЧИСЛОМ, то есть неопределенностью
//        /// </summary>
//        /// <param name="d"></param>
//        /// <returns></returns>
//        public static bool IsNaN(this double d)
//        {
//            return double.IsNaN(d);
//        }

//        /// <summary>
//        /// Четное ли число?
//        /// </summary>
//        /// <param name="i"></param>
//        /// <returns></returns>
//        public static bool IsEven(this int i)
//        {
//            return i % 2 == 0;
//        }

//        /// <summary>
//        /// НЕчетное ли число?
//        /// </summary>
//        /// <param name="i"></param>
//        /// <returns></returns>
//        public static bool IsOdd(this int i)
//        {
//            return i % 2 != 0;
//        }

//        #endregion

//        #region Стоп лоссы
//        /// <summary>
//        /// Дает расчет параболического стопа для позиции. Расчет идет по формуле 
//        /// stop = pos.EntryPrice - delta + (k * x^2) * step, где x - число бар удержания позы.
//        /// </summary>
//        /// <param name="pos">Позиция</param>
//        /// <param name="bar">Номер бара для которого расчитать параболик. Положительное число, не меньше бара входа в позицию.</param>
//        /// <param name="delta">Смещение стартовой точки стопа вверх или вниз от входа. Положительное число или 0.</param>
//        /// <param name="k">Коэффициент меняющий вид параболы. Положительное число.</param>
//        /// <param name="step">Шаг изменения цены стопа. Положительное число.</param>
//        /// <exception cref="ArgumentOutOfRangeException">При неверных аргументах.</exception>
//        /// <returns></returns>
//        public static double ParabolicStop(this IPosition pos, int bar, double delta, double k, double step)
//        {
//            if (bar < pos.EntryBarNum)
//                throw new ArgumentOutOfRangeException("bar", "номер бара должен быть меньше чем бар открытия позиции.");

//            if (k <= 0)
//                throw new ArgumentOutOfRangeException("k", "k должен быть положительным числом.");

//            if (delta < 0)
//                throw new ArgumentOutOfRangeException("delta", "delta должен быть положительным числом или 0.");

//            if (step <= 0)
//                throw new ArgumentOutOfRangeException("step", "step должен быть положительным числом.");

//            var x = bar - pos.EntryBarNum + 1;      // Время удержания позиции
//            var y = x * x * k;

//            if (pos.IsLong)
//                return pos.EntryPrice - delta + y * step;


//            return pos.EntryPrice + delta - y * step;
//        }

//        /// <summary>
//        /// Возвращает истину если время удержания позиции равно или больше чем time.
//        /// </summary>
//        /// <param name="pos">Позиция</param>
//        /// <param name="bar">Номер бара для которого считать время удержания. Положительное число, не меньше бара входа в позицию.</param>
//        /// <param name="time">Сколько разрешено держать позици. Положительный интервал.</param>
//        /// <returns></returns>
//        public static bool TimeStop(this IPosition pos, int bar, TimeSpan time)
//        {
//            if (bar < pos.EntryBarNum)
//                throw new ArgumentOutOfRangeException("bar", "номер бара должен быть меньше чем бар открытия позиции.");

//            if (time > TimeSpan.Zero)
//                throw new ArgumentOutOfRangeException("time", "время удержания позиции должно быть больше 0.");

//            var openDate = pos.EntryBar.Date;
//            var currentDate = pos.Security.Bars[bar].Date;
//            var diff = currentDate - openDate;

//            return (diff >= time);
//        }
//        #endregion
//    }
//}
