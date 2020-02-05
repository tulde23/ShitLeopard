<template>
   <v-dialog v-model="dialogVisible" persistent max-width="290" transition="dialog-bottom-transition">
 
     
      <v-card>
        <v-toolbar dark color="primary">
          <v-icon color="accent">info</v-icon> 
          <v-toolbar-title>
              Confirmation
          </v-toolbar-title>
          <v-spacer></v-spacer>
        </v-toolbar>

    <v-card-text>  <slot name="body"></slot></v-card-text>
    <v-card-actions>
        <v-btn color="success"  @click="okHandler">Ok</v-btn>
            <v-btn color="accent"  @click="cancelHandler">Cancel</v-btn>
         
    </v-card-actions>
  </v-card>
    </v-dialog>
</template>
<script lang="ts" src="./Confirmation.ts">

</script>

