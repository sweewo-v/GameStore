#!/bin/bash
starts=$1
file=$2
env | grep $starts > _temp
sed -i 's#="#: "#g' _temp
sed -i 's/^/  /g' _temp
sed -i "s/$starts//g" _temp
sed -i '/GAMESTORE_ENV_DATA/r _temp' $file
sed -i '/GAMESTORE_ENV_DATA/d' $file