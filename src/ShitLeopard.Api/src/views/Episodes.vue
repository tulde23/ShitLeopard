<template>
<v-container>
    
    <br>

        <v-data-table :items="groupedBySeason"   hide-actions hide-headers>
            <template v-slot:items="props">

               <td>
                 {{props.item}}
               </td>

            </template>
        </v-data-table>
    </div>

</v-container>
</template>

<script lang="ts" src="./Episodes.ts">

</script>
