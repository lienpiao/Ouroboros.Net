using AutoMapper;
using Ouroboros.Model.Dto;

namespace Ouroboros.Model
{
    /// <summary>
    ///  负责将所有实体做一次映射操作
    /// </summary>
    public static class EntityMapper
    {
        static EntityMapper()
        {
            #region 1.将Model和Dto中的所有实体类在AutoMapper内部建立一个关联
            Mapper.Initialize(x => x.CreateMap<SysUser, SysUserDto>());
            Mapper.Initialize(x => x.CreateMap<SysRole, SysRoleDto>());
            Mapper.Initialize(x => x.CreateMap<SysAction, SysActionDto>());
            Mapper.Initialize(x => x.CreateMap<SysUserAction, SysActionDto>());
            #endregion

            #region 2.将Dto和Model中的所有实体类在AutoMapper内部建立一个关联
            Mapper.Initialize(x => x.CreateMap<SysUserDto, SysUser>());
            Mapper.Initialize(x => x.CreateMap<SysRoleDto, SysRole>());
            Mapper.Initialize(x => x.CreateMap<SysActionDto, SysAction>());
            Mapper.Initialize(x => x.CreateMap<SysUserActionDto, SysAction>());
            #endregion
        }

        #region 3.生成所有的实体的两个转换扩展方法
        public static SysUserDto EntityMap(SysUser model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysUser, SysUserDto>(model);
        }
        public static SysUser EntityMap(SysUserDto model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysUserDto, SysUser>(model);
        }

        public static SysRoleDto EntityMap(SysRole model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysRole, SysRoleDto>(model);
        }
        public static SysRole EntityMap(SysRoleDto model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysRoleDto, SysRole>(model);
        }

        public static SysActionDto EntityMap(SysAction model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysAction, SysActionDto>(model);
        }
        public static SysAction EntityMap(SysActionDto model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysActionDto, SysAction>(model);
        }

        public static SysUserActionDto EntityMap(SysUserAction model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysUserAction, SysUserActionDto>(model);
        }
        public static SysUserAction EntityMap(SysUserActionDto model)
        {
            //将一个实体转换成另外一个实体
            return Mapper.Map<SysUserActionDto, SysUserAction>(model);
        }
        #endregion

    }
}
