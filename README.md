Big Data Training Homework
---------------------

##### Content 
0. [Entry Task](#entryTask)  
1. [Home Work #1](#homeWork1) 
2. [Home Work #2](#homeWork2)  

<a name="entryTask"><h2>0. Entry Task</h2></a>

###### *Task*
Find the attached file test-task_dataset_summer_products.cvs with the clothing products dataset. For each product, there are the following fields of interests:
origin_country - country of origin for the products
price - price of the products
rating_count - how many times the product has been rated by user/consumer
rating_five_count - how many times the product has been rated by user/consumer with five stars

Using Programming Language of your choice (Java/Python/Scala), calculate the following metrics for each Country of Origins:
- Average price of product
- Share of five star products

###### *SQL Representation*
select avg(price), (sum(rating_five_count) / sum(rating_count)) * 100 as five_percentage, origin_country
from summer_products
group by origin_country
order by origin_country

###### *Dataset*
In the attached csv file

-------
###### *Result*
Here is a [folder](https://github.com/metravl/BigDataTraining/tree/main/EntryTask/CsvProcessor) with the result code.

<a name="homeWork1"><h2>1. Home Work #1</h2></a>

###### Persistent layer design

In this lab you will make flat star schema ER-diagram.

Use flight delays https://www.kaggle.com/usdot/flight-delays?select=flights.csv to make initial analysis

###### Exercise: Create diagram

Use tool of your preference (draw.io is recommended) to make a ER-diagram.
Follow the next rules:
1. Use every column in the dataset
2. Diagram has to have no complex/custom data types
3. Final design has to follow star schema principles

-------
###### *Result*
Here is a [draw.io file](https://raw.githubusercontent.com/metravl/BigDataTraining/main/HomeWork1/FlightEvents_ER_diagram.drawio) with the result schema.

<a name="homeWork2"><h2>2. Home Work #2</h2></a>

###### Homework

Save bitstamp messages to the local file system on a master node every minute

Folder structure on local file system: /home/nifi/yyyy-MM-dd-HH-mm

!DON'T FORGET TO STOP YOUR PROCESSORS AFTER TESTING!

Good luck!

-------
###### *Result*
Here is a result [Ni-Fi template file](https://raw.githubusercontent.com/metravl/BigDataTraining/main/HomeWork2/HW2_nifi_template.xml) with the result schema.