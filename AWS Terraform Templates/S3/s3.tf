resource "aws_s3_bucket" "b" {
  bucket = "mitritesh14092023"
  acl    = "private"

  versioning {
    enabled = true
  }

  tags = {
    Name        = "S3Test"
    Environment = "QA"
  }
}