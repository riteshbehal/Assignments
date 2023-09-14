resource "aws_sqs_queue" "devops_queue" {
  name                      = "devops-example-queue"
  delay_seconds             = 90
  max_message_size          = 2048
  message_retention_seconds = 86400
  receive_wait_time_seconds = 10
  visibility_timeout_seconds = 5000
  
    tags = {
    Environment = "production"
  }
}