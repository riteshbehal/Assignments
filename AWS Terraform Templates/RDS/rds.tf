resource "aws_db_instance" "boomsql" {
   allocated_storage   = 30
   storage_type        = "gp2"
   identifier          = "pisdatabase"
   engine              = "mysql"
   engine_version      = "8.0.33"
   instance_class      = "db.t2.micro"
   username            = "admin"
   password            = "Passw0rd!123"
   publicly_accessible = true
   skip_final_snapshot = true

   tags = {
     Name = "MyRDS"
   }
 }