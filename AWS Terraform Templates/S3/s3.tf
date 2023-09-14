resource "aws_s3_bucket" "b" {
  bucket = "simplilearn260820230657"
  acl    = "private"

  versioning {
    enabled = true
  }

  tags = {
    Name        = "S3Test"
    Environment = "QA"
  }
}