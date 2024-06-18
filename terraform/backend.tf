terraform {
  backend "s3" {
    bucket = "terraform-state-bucket-2024-06-18"
    key = "state-folder/terraform.ftstate"
    region = "ap-south-1"
  }
}