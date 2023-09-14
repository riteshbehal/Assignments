resource "aws_vpc" "simplilearnterraformvpc"{
    cidr_block = "10.0.0.0/27"
    tags = {
        Name = "cfadminvpc"
    }
}

resource "aws_subnet" "PublicSubnet"{
    vpc_id = aws_vpc.simplilearnterraformvpc.id
    availability_zone = "ap-south-1a"
    cidr_block = "10.0.0.0/28"
}

resource "aws_subnet" "PrivateSubnet"{
    vpc_id = aws_vpc.simplilearnterraformvpc.id
    cidr_block = "10.0.0.16/28"
    map_public_ip_on_launch = false
}


resource "aws_internet_gateway" "cxigw"{
    vpc_id = aws_vpc.simplilearnterraformvpc.id
}

resource "aws_route_table" "PublicRT"{
    vpc_id = aws_vpc.simplilearnterraformvpc.id
    route {
        cidr_block = "0.0.0.0/0"
        gateway_id = aws_internet_gateway.cxigw.id
    }
}
 

resource "aws_route_table_association" "PublicRTAssociation"{
    subnet_id = aws_subnet.PublicSubnet.id
    route_table_id = aws_route_table.PublicRT.id
}