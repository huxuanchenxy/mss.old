namespace netcore ThriftCore

service UserService{
   User Get(1:i32 id)
}
 
struct User
{
    1: required i64 Id;
    2: required string Name;
    3: optional string Remark;
}