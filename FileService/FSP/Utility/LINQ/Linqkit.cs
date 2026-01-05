using LinqKit;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Utility.LINQ
{
    /// <summary>
    /// LinqKit擴充，string可自動加入條件
    /// </summary>
    public static class Linqkit
    {
        /// <summary>
        /// T 回傳參數型別
        /// </summary>
        /// <typeparam name="TEntity">泛型 輸出資料型別</typeparam>
        /// <typeparam name="SearchModel">泛型 輸入資料型別</typeparam>
        /// <param name="searchParameter">搜尋欄位Model</param>
        /// <param name="hasDefaultValue">沒有搜尋條件時是否要預設值</param>
        /// <param name="isAnd">以And或Or組合Where條件</param>
        /// <returns>pred</returns>
        public static ExpressionStarter<TEntity> GetPredicate<TEntity, SearchModel>(this SearchModel searchParameter, bool hasDefaultValue = true, bool isAnd = true)
        {
            var pred = PredicateBuilder.New<TEntity>(hasDefaultValue);

            if (searchParameter == null)
            {
                return pred;
            }

            foreach (PropertyInfo item in searchParameter.GetType().GetProperties())
            {
                string columnName = item.Name;

                if (item.PropertyType == typeof(string) && typeof(TEntity).GetProperty(columnName) != null)
                {
                    var value = item.GetValue(searchParameter, null);

                    if (value != null)
                    {
                        Type dbType = typeof(TEntity).GetProperty(columnName).PropertyType;
                        Type paraType = item.PropertyType;

                        if (dbType == paraType)
                        {
                            MethodInfo method = typeof(TEntity).GetProperty(columnName).PropertyType.GetMethod("Contains", new[] { dbType });
                            var someValue = Expression.Constant(value, dbType);
                            var parameterExp = Expression.Parameter(typeof(TEntity), "type");
                            var propertyExp = Expression.Property(parameterExp, columnName);
                            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

                            if(isAnd)
                            {
                                pred = pred.And(Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameterExp));
                            }
                            else
                            {
                                pred = pred.Or(Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameterExp));
                            }
                        }
                    }
                }
            }

            return pred;
        }
    }
}