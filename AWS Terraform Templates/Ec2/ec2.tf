resource "aws_instance" "web" {
  ami           = "ami-0ff1768d0d7c69c2a"
  instance_type = "t2.micro"
  security_groups = [aws_security_group.simplilearndev1.name]
  key_name = aws_key_pair.key_pair.key_name

  tags = {
    Name = "HelloWorld"
  }
}

resource "aws_security_group" "simplilearndev1" {
  name        = "security group using simplilearndev1"
  description = "security group using simplilearndev1"
  vpc_id      = "vpc-072c4acd761c3b942"

  ingress {
    description      = "HTTPS"
    from_port        = 443
    to_port          = 443
    protocol         = "tcp"
    cidr_blocks      = ["0.0.0.0/0"]
   }

    ingress {
    description      = "HTTP"
    from_port        = 80
    to_port          = 80
    protocol         = "tcp"
    cidr_blocks      = ["0.0.0.0/0"]
  }

  ingress {
    description      = "RDP"
    from_port        = 3389
    to_port          = 3389
    protocol         = "tcp"
    cidr_blocks      = ["0.0.0.0/0"]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
  }

  tags = {
    Name = "simplilearndev1"
  }
}