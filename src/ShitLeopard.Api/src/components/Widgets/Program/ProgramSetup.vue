<template>
<div>
 

   <v-form>
    <v-container>
      <v-layout row wrap>

        <v-flex xs12 sm3 md3 buffer>
              <v-select :items="activityTypes" @change="onActivityTypesChanged" :loading="busy"
                                                          v-model="activityTypeId"
                                                          label="Activity Type"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="id">
                                                </v-select>
            
        </v-flex>

        <v-flex xs12 sm3 md3 buffer>
       <v-select :items="affiliations" @change="onAffiliationsChanged" :disabled="!activityTypeId"  :loading="busy"
                                                          v-model="sportsAffiliationId"
                                                          label="National Affiliation"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="id">
                                                </v-select>
        </v-flex>

      

        <v-flex xs12 sm3 md3 buffer>
 <v-select :items="organizations" @change="onOrganizationsChanged" :disabled="!sportsAffiliationId"   :loading="busy"
                                                          v-model="organizationId"
                                                          label="Organizations"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="organizationId">
                                                </v-select>
        </v-flex>

        <v-flex xs12 sm3 md3 buffer v-if="showTier1">
   <v-select :items="tier1districts"  @change="onTier1Changed" :disabled="!organizationId"   :loading="busy"
                                                          v-model="tier1DistrictId"
                                                          label="Tier 1 Districts"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="tier1DistrictId">
                                                </v-select>
        </v-flex>
           <v-flex xs12 sm3 md3 buffer v-if="showTier2">
         <v-select :items="tier2Leagues"   @change="onTier2Changed" :disabled="!organizationId"   :loading="busy"
                                                          v-model="tier2LeagueId"
                                                          label="Tier 2 Leagues"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="tier2LeagueId">
                                                </v-select>
        </v-flex>
           <v-flex xs12 sm3 md3 buffer v-if="showTier3">
            <v-select :items="tier3Clubs"   @change="onTier3Changed" :disabled="!tier2LeagueId"   :loading="busy"
                                                          v-model="tier3ClubId"
                                                          label="Tier 3 Clubs"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="tier3ClubId">
                                                </v-select>
        </v-flex>

  <v-flex xs12 sm3 md3 buffer v-if="showSeason">
            <v-select :items="seasons"   @change="onSeasonChanged" :disabled="!tier2LeagueId"   :loading="busy"
                                                          v-model="seasonId"
                                                          label="Seasons"
                                                          class="input-group--focused"
                                                          item-text="displayName"
                                                          item-value="seasonId">
                                                </v-select>
        </v-flex>

  <v-flex xs12 sm3 md3 buffer v-if="showPlayLevel">
            <v-select :items="playLevels"  @change="onPlayLevelChanged"  :disabled="!seasonId"    :loading="busy"
                                                          v-model="playLevelId"
                                                          label="Play Levels"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="playLevelId">
                                                </v-select>
        </v-flex>
          <v-flex xs12 sm3 md3 buffer v-if="showPlayType">
            <v-select :items="playTypes"  @change="onPlayTypeChanged" :disabled="!seasonId"   :loading="busy"
                                                          v-model="playTypeId"
                                                          label="Play Types"
                                                          class="input-group--focused"
                                                          item-text="name"
                                                          item-value="playTypeId">
                                                </v-select>
        </v-flex>
           <v-flex xs12 sm3 md3 buffer>
           <slot name="filter1"></slot>
        </v-flex>
          <v-flex xs12 sm3 md3 buffer>
           <slot name="filter2"></slot>
        </v-flex>
      </v-layout>
    </v-container>

  </v-form>  
  </div>
</template>
<script lang="ts" src="./ProgramSetup.ts">

</script>
<style lang="scss" src="./ProgramSetup.scss" scoped>
</style>