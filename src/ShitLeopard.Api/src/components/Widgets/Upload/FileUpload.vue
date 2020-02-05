<template>
<div>
   
   <v-btn color="success" @click="$refs.inputUpload.click()">Upload</v-btn>
    <input v-show="false" ref="inputUpload" type="file" @change="onFileChange" accept="text/json" >
        
        <table style="padding:1.5%; margin:0" width="100%">
          <tr v-for="(item,index) in uploadedFiles" :key="index">
              <td> {{item.email}}</td> <td>
                  <template v-if="item.errors && item.errors.length>0">
<ol v-if="item.errors">
                    <li v-for="(i,ix) in item.errors" :key="ix">
                        {{i}}
                    </li>
                </ol>
                  </template>
                  <template v-else>
                      Success!
                  </template>
              </td>
          </tr>
        </table>
    
  
</div>
</template>
<script lang="ts" src="./FileUpload.ts"></script>
<style lang="scss" src="./FileUpload.scss" scoped>
</style>
