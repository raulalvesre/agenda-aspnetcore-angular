using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.Internal;

namespace Agenda.Application.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> MergeList<TSource, TDestination, TDestMember, TSrcMember>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, IEnumerable<TDestMember>>> srcMember,
            Expression<Func<TSource, IEnumerable<TSrcMember>>> vmMember
        )
        {
            return MergeList(map, srcMember, vmMember, (dest, result) =>
            {
                var member = srcMember.GetMember();
                member.SetMemberValue(dest, result);
            });
        }
        public static IMappingExpression<TSource, TDestination> MergeList<TSource, TDestination, TDestMember, TSrcMember>(
            this IMappingExpression<TSource, TDestination> map,
            Expression<Func<TDestination, IEnumerable<TDestMember>>> srcMember,
            Expression<Func<TSource, IEnumerable<TSrcMember>>> vmMember,
            Action<TDestination, IEnumerable<TDestMember>> setFunction
        )
        {
            return map
                .ForMember(srcMember, m => m.Ignore())
                .AfterMap((vm, src, context) =>
                {
                    var srcList = srcMember.Compile().Invoke(src);
                    var vmList = vmMember.Compile().Invoke(vm);

                    if (vmList == null)
                        throw new InvalidOperationException(nameof(vmList));

                    if (srcList == null)
                    {
                        setFunction(src, context.Mapper.Map<IEnumerable<TDestMember>>(vmList));
                    }

                    else
                    {
                        int l = 0;
                        var array = srcList.ToArray();
                        var result = vmList.Select(t =>
                        {
                            TDestMember result;
                            if (l < array.Length)
                            {
                                result = context.Mapper.Map(t, array[l]);
                            }
                            else
                            {
                                result = context.Mapper.Map<TDestMember>(t);
                            }

                            l++;
                            return result;
                        }).ToList();
                        setFunction(src, result);
                    }
                });
        }
    }
}
