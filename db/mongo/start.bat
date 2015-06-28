@echo off

echo starting server
start mongod.exe --config=mongo.cfg

echo starting console client
mongo.exe